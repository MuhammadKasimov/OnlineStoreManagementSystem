using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineStoreManagementSystem.Data.IRepositories;
using OnlineStoreManagementSystem.Data.Repositories;
using OnlineStoreManagementSystem.Domain.Entitties.Attachments;
using OnlineStoreManagementSystem.Domain.Entitties.Carts;
using OnlineStoreManagementSystem.Domain.Entitties.Products;
using OnlineStoreManagementSystem.Domain.Entitties.Users;
using OnlineStoreManagementSystem.Service.Interfaces.Attachments;
using OnlineStoreManagementSystem.Service.Interfaces.Products;
using OnlineStoreManagementSystem.Service.Interfaces.Users;
using OnlineStoreManagementSystem.Service.Services.Attachments;
using OnlineStoreManagementSystem.Service.Services.Products;
using OnlineStoreManagementSystem.Service.Services.Users;
using System.Linq;
using System.Text;

namespace OnlineStoreManagementSystem.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            // repositories
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            services.AddScoped<IGenericRepository<UserProduct>, GenericRepository<UserProduct>>();
            services.AddScoped<IGenericRepository<Cart>, GenericRepository<Cart>>();
            services.AddScoped<IGenericRepository<Attachment>, GenericRepository<Attachment>>();


            // services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
        }

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))

                };
            });
        }

        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(p =>
            {
                p.ResolveConflictingActions(ad => ad.First());
                p.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                });

                p.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }
    }
}
