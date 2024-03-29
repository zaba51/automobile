using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Entities;
using Microsoft.IdentityModel.Tokens;

namespace backend.Helpers
{
    public static class JWTHelper
    {
        public static string SignIn(User user, IConfiguration _configuration)
            {
                
                var securityKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"])
                );

                var signingCredentials = new SigningCredentials(
                    securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim("sub", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("supplierId", user.SupplierId.ToString()),
                };

                var jwtSecurityToken = new JwtSecurityToken(
                    _configuration["Authentication:Issuer"],
                    _configuration["Authentication:Audience"],
                    claims,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    signingCredentials);

                var tokenToReturn = new JwtSecurityTokenHandler()
                    .WriteToken(jwtSecurityToken);

                return tokenToReturn;
            }
    }
}