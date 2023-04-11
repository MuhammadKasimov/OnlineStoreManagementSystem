using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineStoreManagementSystem.Data.IRepositories;
using OnlineStoreManagementSystem.Domain.Entitties.Users;
using OnlineStoreManagementSystem.Service.Exceptions;
using OnlineStoreManagementSystem.Service.Extensions;
using OnlineStoreManagementSystem.Service.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Service.Services.Users
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> userRepository;
        private readonly IConfiguration configuration;

        public AuthService(IGenericRepository<User> userRepository,
            IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
        }

        public async ValueTask<string> GenerateToken(string email, string password)
        {
            User user = await userRepository.GetAsync(u =>
                u.Username == email && u.Password.Equals(password.Encrypt()));

            if (user is null)
                throw new HttpStatusCodeException(400, "Login or Password is incorrect");

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddHours(int.Parse(configuration["JWT:Expire"])),
                claims: new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                },
                signingCredentials: new SigningCredentials(
                    key: authSigningKey,
                    algorithm: SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
