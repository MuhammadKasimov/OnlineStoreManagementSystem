using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineStoreManagementSystem.Api.Extensions;
using OnlineStoreManagementSystem.Api.Helpers;
using OnlineStoreManagementSystem.Api.Middlewares;
using OnlineStoreManagementSystem.Contexts;
using OnlineStoreManagementSystem.Service.Helpers;
using OnlineStoreManagementSystem.Service.Mappers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(
                                 new ConfigureApiUrlName()));
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization();

builder.Services.AddSignalR();

// Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add custom services
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddSwaggerService();
builder.Services.AddCustomServices();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

// Set helpers
EnvironmentHelper.WebRootPath = app.Services.GetRequiredService<IWebHostEnvironment>()?.WebRootPath;

if (app.Services.GetService<IHttpContextAccessor>() != null)
    HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();



app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
