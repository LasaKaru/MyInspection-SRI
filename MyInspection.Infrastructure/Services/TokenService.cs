using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyInspection.Core.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Infrastructure.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            // Ensure the key is not null or empty before creating SymmetricSecurityKey
            var keyString = _config["Jwt:Key"];
            if (string.IsNullOrEmpty(keyString))
            {
                throw new InvalidOperationException("JWT Key is not configured.");
            }
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.Email, user.Email),
                //new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
                // THIS IS THE CRITICAL FIX: Add the User ID
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                
                // It's also good practice to use Name instead of GivenName for the username
                new Claim(ClaimTypes.Name, user.UserName),

                // JwtRegisteredClaimNames are standardized, but ClaimTypes are more common in .NET
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
