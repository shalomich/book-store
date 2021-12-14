
using BookStore.Application.Dto;
using BookStore.Application.Providers;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class JwtConverter
    {
        private readonly IJwtProvider _jwtProvider;

        public JwtConverter(IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
        }

        public string ToToken(AuthorizedDataDto authorizedData)
        {
            var (id, email, role) = authorizedData;
            
            var claims = new List<Claim> {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role.ToString()),
                new Claim(nameof(AuthorizedDataDto.Id), id.ToString())
            };

            var (tokenKey, expiredMinutes, _) = _jwtProvider.GenerateSettings();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(expiredMinutes),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public AuthorizedDataDto FromToken(string token)
        {
            string tokenKey = _jwtProvider.GenerateSettings().TokenKey;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, 
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            
            if (jwtSecurityToken == null)
                throw new SecurityTokenException("Invalid token");

            int id = int.Parse(principal.Claims
                .Single(claim => claim.Type == nameof(AuthorizedDataDto.Id))
                .Value);

            string email = principal.Identity.Name;

            var role = Enum.Parse<UserRole>(principal.Claims
                .Single(claim => claim.Type == ClaimTypes.Role)
                .Value);

            return new AuthorizedDataDto(id, email, role);
        }

    }
}
