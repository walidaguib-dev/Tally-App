using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.InfrastructureTests.Repositories.Tokens
{
    public class TokensRepository_GenerateAccessToken_Tests
    {
        private static Mock<UserManager<User>> CreateMockUserManager()
        {
            var store = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(
                store.Object,
                null!, // IOptions<IdentityOptions>
                null!, // IPasswordHasher<TUser>
                null!, // IEnumerable<IUserValidator<TUser>>
                null!, // IEnumerable<IPasswordValidator<TUser>>
                null!, // ILookupNormalizer
                null!, // IdentityErrorDescriber
                null!, // IServiceProvider
                null!  // ILogger<UserManager<TUser>>
            );
        }

        private static ApplicationDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task GenerateAccessToken_ValidToken_ReturnsJwt_AndRotatesRefreshToken()
        {
            // Arrange
            using var context = CreateInMemoryContext();

            var mockUserManager = CreateMockUserManager();
            mockUserManager
                .Setup(m => m.GetRolesAsync(It.IsAny<User>()))
                .ReturnsAsync(new List<string> { "User" });

            var inMemorySettings = new Dictionary<string, string>
            {
                ["JWT:Key"] = "very-long-test-key-for-jwt-signing-0123456789",
                ["JWT:Issuer"] = "unittest-issuer",
                ["JWT:Audience"] = "unittest-audience",
                ["JWT:AccessTokenExpirationMinutes"] = "60"
            };

            var configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings!).Build();

            var repo = new TokensRepository(context, mockUserManager.Object, configuration);

            var user = new User { Id = "u-valid", UserName = "u-valid", Email = "u-valid@example.com" };
            context.Users.Add(user);

            var oldRefresh = new RefreshToken
            {
                Token = "existing-refresh",
                userId = user.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                ExpiresAt = DateTime.UtcNow.AddDays(6)
            };


            context.refreshTokens.Add(oldRefresh);
            await context.SaveChangesAsync();

            // Act
            var jwt = await repo.GenerateAccessToken(user.Id, "existing-refresh");

            // Assert - jwt shape
            Assert.False(string.IsNullOrWhiteSpace(jwt));
            Assert.Equal(3, jwt.Split('.').Length);

            // Refresh token should have been rotated (token string changed) and persisted
            var refreshed = context.refreshTokens.Single(rt => rt.userId == user.Id);
            Assert.NotNull(refreshed);
            Assert.NotEqual("existing-refresh", refreshed.Token);
            Assert.True(refreshed.ExpiresAt > refreshed.CreatedAt);
            // Implementation updates the same DB row, so RevokedAt will be reset to null after rotation
            Assert.Null(refreshed.RevokedAt);
        }

        [Fact]
        public async Task GenerateAccessToken_InvalidToken_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var mockUserManager = CreateMockUserManager();
            var mockConfig = new Mock<IConfiguration>();
            var repo = new TokensRepository(context, mockUserManager.Object, mockConfig.Object);

            var user = new User { Id = "u-invalid", UserName = "u-invalid", Email = "u-invalid@example.com" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => repo.GenerateAccessToken(user.Id, "non-existent-token"));
        }

        [Fact]
        public async Task GenerateAccessToken_ExpiredOrRevoked_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var mockUserManager = CreateMockUserManager();
            var mockConfig = new Mock<IConfiguration>();
            var repo = new TokensRepository(context, mockUserManager.Object, mockConfig.Object);

            var user = new User { Id = "u-exp", UserName = "u-exp", Email = "u-exp@example.com" };
            context.Users.Add(user);

            var expired = new RefreshToken
            {
                Token = "expired-token",
                userId = user.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                ExpiresAt = DateTime.UtcNow.AddDays(-5)
            };

            var revoked = new RefreshToken
            {
                Token = "revoked-token",
                userId = user.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                ExpiresAt = DateTime.UtcNow.AddDays(5),
                RevokedAt = DateTime.UtcNow.AddDays(-1)
            };

            context.refreshTokens.Add(expired);
            context.refreshTokens.Add(revoked);
            await context.SaveChangesAsync();

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => repo.GenerateAccessToken(user.Id, "expired-token"));
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => repo.GenerateAccessToken(user.Id, "revoked-token"));
        }

        [Fact]
        public async Task GenerateAccessToken_RefreshTokenExistsButUserMissing_ReturnsNull()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var mockUserManager = CreateMockUserManager();
            var mockConfig = new Mock<IConfiguration>();
            var repo = new TokensRepository(context, mockUserManager.Object, mockConfig.Object);

            // Add a refresh token referencing a user id that does NOT exist in Users table.
            var orphanRefresh = new RefreshToken
            {
                Token = "orphan-token",
                userId = "missing-user",
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
            context.refreshTokens.Add(orphanRefresh);
            await context.SaveChangesAsync();

            // Act
            var result = await repo.GenerateAccessToken("missing-user", "orphan-token");

            // Assert
            Assert.Null(result);
        }
    }
}
