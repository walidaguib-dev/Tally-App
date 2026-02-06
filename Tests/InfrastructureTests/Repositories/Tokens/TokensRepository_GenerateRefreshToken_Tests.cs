using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.InfrastructureTests.Repositories.Tokens
{
    public class TokensRepository_GenerateRefreshToken_Tests
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
        public async Task GenerateRefreshToken_WhenNoExistingToken_CreatesNewRefreshTokenAndPersists()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var mockUserManager = CreateMockUserManager();
            var mockConfig = new Mock<IConfiguration>();
            var repo = new TokensRepository(context, mockUserManager.Object, mockConfig.Object);

            var user = new User { Id = "user-new", UserName = "user-new", Email = "new@example.com" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            // Act
            var refreshToken = await repo.GenerateRefreshToken(user);

            // Assert
            Assert.NotNull(refreshToken);
            Assert.Equal(user.Id, refreshToken.userId);
            Assert.False(string.IsNullOrWhiteSpace(refreshToken.Token));
            Assert.True(refreshToken.ExpiresAt > refreshToken.CreatedAt);
            Assert.Null(refreshToken.RevokedAt);

            // persisted
            var persisted = context.refreshTokens.SingleOrDefault(rt => rt.Id == refreshToken.Id);
            Assert.NotNull(persisted);
            Assert.Equal(refreshToken.Token, persisted.Token);
        }

        [Fact]
        public async Task GenerateRefreshToken_WhenExistingToken_UpdatesAndReturnsLatestToken()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var mockUserManager = CreateMockUserManager();
            var mockConfig = new Mock<IConfiguration>();
            var repo = new TokensRepository(context, mockUserManager.Object, mockConfig.Object);

            var user = new User { Id = "user-existing", UserName = "user-existing", Email = "existing@example.com" };
            context.Users.Add(user);

            var existing = new RefreshToken
            {
                Token = "old-token-value",
                userId = user.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                ExpiresAt = DateTime.UtcNow.AddDays(-3),
                RevokedAt = DateTime.UtcNow.AddDays(-5)
            };

            context.refreshTokens.Add(existing);
            await context.SaveChangesAsync();

            var oldId = existing.Id;
            var oldTokenString = existing.Token;
            var oldCreated = existing.CreatedAt;

            // Act
            var updated = await repo.GenerateRefreshToken(user);

            // Assert - ensure the repository returned a token and the persisted/latest entry reflects the returned token
            Assert.NotNull(updated);
            var persistedLatest = context.refreshTokens
                .Where(rt => rt.userId == user.Id)
                .OrderByDescending(rt => rt.CreatedAt)
                .First();

            Assert.Equal(updated.Id, persistedLatest.Id);
            Assert.Equal(user.Id, persistedLatest.userId);
            Assert.NotEqual(oldTokenString, persistedLatest.Token); // token rotated (or replaced)
            Assert.True(persistedLatest.ExpiresAt > persistedLatest.CreatedAt);
            Assert.True(persistedLatest.CreatedAt >= oldCreated); // createdAt updated or equal
            Assert.Null(persistedLatest.RevokedAt); // reset revocation
        }

        [Fact]
        public async Task GenerateRefreshToken_MultipleCalls_ReturnsLatestTokenAndPersistsLatest()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var mockUserManager = CreateMockUserManager();
            var mockConfig = new Mock<IConfiguration>();
            var repo = new TokensRepository(context, mockUserManager.Object, mockConfig.Object);

            var user = new User { Id = "user-multi", UserName = "user-multi", Email = "multi@example.com" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var first = await repo.GenerateRefreshToken(user);
            var second = await repo.GenerateRefreshToken(user);

            // Assert: ensure both calls returned tokens and the persisted latest equals the second result
            Assert.NotNull(first);
            Assert.NotNull(second);

            var persistedLatest = context.refreshTokens
                .Where(rt => rt.userId == user.Id)
                .OrderByDescending(rt => rt.CreatedAt)
                .First();

            Assert.Equal(second.Id, persistedLatest.Id);
            Assert.Equal(second.Token, persistedLatest.Token);
        }
    }
}