using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using FluentEmail.Core;
using Hangfire;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.InfrastructureTests.Repositories.Emails
{
    public class EmailsRepository_SendEmail_Tests
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
        public async Task SendEmail_UserNotFound_ReturnsNull_AndDoesNotEnqueue()
        {
            // Arrange
            var userId = "missing-user";
            var mockUserManager = CreateMockUserManager();
            var context = CreateInMemoryContext();
            var mockFluent = new Mock<IFluentEmail>();
            var mockJobClient = new Mock<IBackgroundJobClient>();
            var mockConfig = new Mock<IConfiguration>();

            // ensure Users is an EF-backed IQueryable so FirstOrDefaultAsync works
            mockUserManager.Setup(m => m.Users).Returns(context.Users);

            // FindByIdAsync returns null
            mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((User?)null);

            var repo = new EmailsRepository(
                mockUserManager.Object,
                context,
                mockFluent.Object,
                mockJobClient.Object,
                mockConfig.Object
            );

            // Act
            var result = await repo.SendEmail(userId, VerificationPurpose.EmailVerification);

            // Assert
            Assert.Null(result);
            Assert.Empty(context.emailTokens.ToList());
            Assert.Empty(mockJobClient.Invocations); // extension method cannot be verified directly, confirm no invocations
        }

        [Fact]
        public async Task SendEmail_Success_PersistsEmailToken_AndEnqueuesJob()
        {
            // Arrange
            var userId = "user-1";
            var user = new User { Id = userId, Email = "a@b.com", UserName = "u" };

            var mockUserManager = CreateMockUserManager();
            var context = CreateInMemoryContext();
            var mockFluent = new Mock<IFluentEmail>();
            var mockJobClient = new Mock<IBackgroundJobClient>();
            var mockConfig = new Mock<IConfiguration>();

            // Make Users an EF-backed IQueryable and add the user to the in-memory context
            mockUserManager.Setup(m => m.Users).Returns(context.Users);
            context.Users.Add(user);
            await context.SaveChangesAsync();

            // Utils.GetEmail will call FindByIdAsync + GenerateEmailConfirmationTokenAsync (for EmailVerification)
            mockUserManager.Setup(m => m.FindByIdAsync(It.Is<string>(s => s == userId))).ReturnsAsync(user);
            mockUserManager.Setup(m => m.GenerateEmailConfirmationTokenAsync(It.Is<User>(u => u == user)))
                .ReturnsAsync("generated-token-hash");

            var repo = new EmailsRepository(
                mockUserManager.Object,
                context,
                mockFluent.Object,
                mockJobClient.Object,
                mockConfig.Object
            );

            // Act
            var result = await repo.SendEmail(userId, VerificationPurpose.EmailVerification);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result!.UserId);
            Assert.Equal(VerificationPurpose.EmailVerification, result.Purpose);
            Assert.Equal("generated-token-hash", result.CodeHash);

            // persisted
            var persisted = context.emailTokens.SingleOrDefault(t => t.UserId == userId && t.CodeHash == result.CodeHash);
            Assert.NotNull(persisted);

            // job enqueued -> extension method can't be verified directly, assert the mock received an invocation
            Assert.Single(mockJobClient.Invocations);
        }

        [Fact]
        public async Task SendEmail_GetEmailReturnsNull_ThrowsInvalidOperationException()
        {
            // Arrange
            var userId = "user-2";
            var user = new User { Id = userId, Email = "b@b.com", UserName = "u2" };

            var mockUserManager = CreateMockUserManager();
            var context = CreateInMemoryContext();
            var mockFluent = new Mock<IFluentEmail>();
            var mockJobClient = new Mock<IBackgroundJobClient>();
            var mockConfig = new Mock<IConfiguration>();

            // EF-backed Users and ensure user exists in the context
            mockUserManager.Setup(m => m.Users).Returns(context.Users);
            context.Users.Add(user);
            await context.SaveChangesAsync();

            mockUserManager.Setup(m => m.FindByIdAsync(It.Is<string>(s => s == userId))).ReturnsAsync(user);

            // Simulate token generation producing null so Utils.GetEmail returns null
            mockUserManager.Setup(m => m.GenerateEmailConfirmationTokenAsync(It.Is<User>(u => u == user)))
                .ReturnsAsync((string?)null);

            var repo = new EmailsRepository(
                mockUserManager.Object,
                context,
                mockFluent.Object,
                mockJobClient.Object,
                mockConfig.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => repo.SendEmail(userId, VerificationPurpose.EmailVerification));

            // ensure nothing persisted or enqueued
            Assert.Empty(context.emailTokens.ToList());
            Assert.Empty(mockJobClient.Invocations);
        }
    }
}