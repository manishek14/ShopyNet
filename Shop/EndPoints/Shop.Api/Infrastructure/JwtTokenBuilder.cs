using Microsoft.IdentityModel.Tokens;
using Shop.Query.User.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shop.Api.Infrastructure.JwtUtil
{
    public static class JwtTokenBuilder
    {
        public static string BuildToken(UserDto user, JwtConfig jwtConfig)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name} {user.Family}".Trim()),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            };

            if (user.Roles != null && user.Roles.Any())
            {
                foreach (var role in user.Roles)
                {
                    if (!string.IsNullOrWhiteSpace(role.RoleTitle))
                        claims.Add(new Claim(ClaimTypes.Role, role.RoleTitle));
                }
            }

            var secretKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtConfig.SignInKey));

            var credentials = new SigningCredentials(
                secretKey,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtConfig.Issuer,
                audience: jwtConfig.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(jwtConfig.TokenExpireDays),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}