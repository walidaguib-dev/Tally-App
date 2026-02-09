using Domain.Entities;
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
using System.Text;

namespace Tests.InfrastructureTests.Repositories.Emails
{
    public class EmailsRepository_ConfirmEmailAsync_Tests
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


        [Theory]
        // userFound, confirmSucceeds, shouldThrow, expectedResult
        [InlineData(false, false, false, null)] // user not found -> returns null
        [InlineData(true, true, false, "Done")]  // user found & confirm succeeds -> "Done"
        [InlineData(true, false, true, null)]    // user found & confirm fails -> throws

        public async Task ConfirmEmailAsync_Theory(bool userFound, bool confirmSucceeds, bool shouldThrow, string? expected)
        {
            // Arrange
            var userId = "user-123";
            var token = "token-abc";

            var mockUserManager = CreateMockUserManager();
            var context = CreateInMemoryContext();
            var mockFluent = new Mock<IFluentEmail>();
            var mockJobClient = new Mock<IBackgroundJobClient>();
            var mockConfig = new Mock<IConfiguration>();

            User? user = userFound ? new User { Id = userId, Email = "a@b.com", UserName = "u" } : null;

            mockUserManager
                .Setup(m => m.FindByIdAsync(It.Is<string>(s => s == userId)))
                .ReturnsAsync(user);
            if (userFound)
            {
                mockUserManager
                    .Setup(m => m.ConfirmEmailAsync(It.Is<User>(u => u == user), It.Is<string>(t => t == token)))
                    .ReturnsAsync(confirmSucceeds ? IdentityResult.Success : IdentityResult.Failed(new IdentityError { Description = "Invalid token" }));
            }

            var repo = new EmailsRepository(
                mockUserManager.Object,
                context,
                mockFluent.Object,
                mockJobClient.Object,
                mockConfig.Object
            );

            // Act & Assert
            if (shouldThrow)
            {
                var ex = await Assert.ThrowsAsync<System.Exception>(() => repo.ConfirmEmailAsync(userId, token));
                Assert.Contains("Invalid or expired token", ex.Message);
            }
            else
            {
                var result = await repo.ConfirmEmailAsync(userId, token);
                Assert.Equal(expected, result as string);
            }
        }
        }
}
