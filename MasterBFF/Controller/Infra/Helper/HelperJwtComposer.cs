using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using Master.Entity;
using Master.Entity.Dto.Infra;

namespace Master.Controller.Helper
{
    public class HelperJwtComposer
    {
        [NonAction]
        public string ComposeTokenForSession(DtoAuthenticatedUser currentAuth)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(LocalNetwork.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user", JsonSerializer.Serialize(currentAuth)),
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(
                                        new SymmetricSecurityKey(key),
                                        SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
