using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Repositories
{
    public class TokensRepository
        (
          ApplicationDbContext context,
          UserManager<User> userManager,
          IConfiguration configuration
        ) : ITokens
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<User> _userManager = userManager;
        private readonly IConfiguration _configuration = configuration;
        public async Task<string?> GenerateAccessToken(string userId, string token)
        {
            // Fetch the refresh token from DB and ensure it belongs to the user
            var refreshToken = await _context.refreshTokens
                .FirstOrDefaultAsync(rt => rt.userId == userId && rt.Token == token);
            if (refreshToken == null)
                throw new UnauthorizedAccessException("Invalid refresh token.");
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return null;

            if (refreshToken.RevokedAt != null || refreshToken.ExpiresAt <= DateTime.UtcNow)
                throw new UnauthorizedAccessException("Refresh token is expired or revoked.");

            refreshToken.RevokedAt = DateTime.UtcNow;
            var newRefreshToken = await GenerateRefreshToken(user);
            await _context.SaveChangesAsync();

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email ?? ""),

            };

            foreach (var role in roles) { claims.Add(new Claim(ClaimTypes.Role, role)); }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessTokenExpirationMinutes = int.Parse(_configuration["JWT:AccessTokenExpirationMinutes"] ?? "15");
            var jwtToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(accessTokenExpirationMinutes),
                SigningCredentials = creds,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tk = tokenHandler.CreateToken(jwtToken);
            return tokenHandler.WriteToken(tk);
        }

        public async Task<RefreshToken> GenerateRefreshToken(User user)
        {
            // Generate a secure random token string
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            var tokenString = Convert.ToBase64String(randomBytes);

            var existingToken = await _context.refreshTokens
                .FirstOrDefaultAsync(rt => rt.userId == user.Id);


            if (existingToken != null)
            {
                // Update existing token
                existingToken.Token = tokenString;
                existingToken.CreatedAt = DateTime.UtcNow;
                existingToken.ExpiresAt = DateTime.UtcNow.AddDays(7);
                existingToken.RevokedAt = null; // reset revocation
                await _context.SaveChangesAsync();

                return existingToken;
            }
            else
            {
                // Create new token if none exists
                var refreshToken = new RefreshToken
                {
                    Token = tokenString,
                    userId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddDays(7)
                };
                _context.refreshTokens.Add(refreshToken);
                await _context.SaveChangesAsync();

                return refreshToken;
            }
        }
    }
}
