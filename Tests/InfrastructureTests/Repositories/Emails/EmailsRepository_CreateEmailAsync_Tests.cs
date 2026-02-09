using Domain.Entities;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using Hangfire;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.InfrastructureTests.Repositories.Emails
{
    public class EmailsRepository_CreateEmailAsync_Tests
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
        public async Task CreateEmailAsync_EmailVerification_BuildsBodyAndSendsEmail()
        {
            // Arrange
            var recipient = "alice@example.com";
            var subject = "Please verify";
            var userId = "user-1";
            var token = "tok-123";

            var mockUserManager = CreateMockUserManager();
            var context = CreateInMemoryContext();
            var mockJobClient = new Mock<IBackgroundJobClient>();

            var inMemorySettings = new Dictionary<string, string?>
            {
                ["ApiBaseUrl"] = "https://app.test"
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            var mockFluent = new Mock<IFluentEmail>();
            // chainable fluent API setup
            mockFluent.Setup(f => f.To(It.Is<string>(s => s == recipient))).Returns(mockFluent.Object);
            mockFluent.Setup(f => f.Subject(It.Is<string>(s => s == subject))).Returns(mockFluent.Object);
            mockFluent.Setup(f => f.Body(It.IsAny<string>(), It.IsAny<bool>())).Returns(mockFluent.Object);
            mockFluent.Setup(f => f.SendAsync()).ReturnsAsync(new SendResponse());

            var repo = new EmailsRepository(
                mockUserManager.Object,
                context,
                mockFluent.Object,
                mockJobClient.Object,
                configuration
            );

            // Act
            await repo.CreateEmailAsync(recipient, subject, userId, token, Domain.Enums.VerificationPurpose.EmailVerification);

            // Assert
            mockFluent.Verify(f => f.To(It.Is<string>(s => s == recipient)), Times.Once);
            mockFluent.Verify(f => f.Subject(It.Is<string>(s => s == subject)), Times.Once);
            // Body should contain userId and token (token will be URL-escaped)
            mockFluent.Verify(f => f.Body(It.Is<string>(b => b.Contains(userId) && b.Contains(Uri.EscapeDataString(token))), true), Times.Once);
            mockFluent.Verify(f => f.SendAsync(), Times.Once);

        }

        [Fact]
        public async Task CreateEmailAsync_PasswordReset_BuildsBodyContainingToken_AndSendsEmail()
        {
            // Arrange
            var recipient = "bob@example.com";
            var subject = "Reset your password";
            var userId = "user-2";
            var token = "reset-token-xyz";

            var mockUserManager = CreateMockUserManager();
            var context = CreateInMemoryContext();
            var mockJobClient = new Mock<IBackgroundJobClient>();
            var configuration = new ConfigurationBuilder().Build(); // no ApiBaseUrl needed for password reset body

            var mockFluent = new Mock<IFluentEmail>();
            mockFluent.Setup(f => f.To(It.Is<string>(s => s == recipient))).Returns(mockFluent.Object);
            mockFluent.Setup(f => f.Subject(It.Is<string>(s => s == subject))).Returns(mockFluent.Object);
            mockFluent.Setup(f => f.Body(It.IsAny<string>(), It.IsAny<bool>())).Returns(mockFluent.Object);
            mockFluent.Setup(f => f.SendAsync()).ReturnsAsync(new SendResponse());

            var repo = new EmailsRepository(
                mockUserManager.Object,
                context,
                mockFluent.Object,
                mockJobClient.Object,
                configuration
            );

            // Act
            await repo.CreateEmailAsync(recipient, subject, userId, token, Domain.Enums.VerificationPurpose.PasswordReset);

            // Assert
            mockFluent.Verify(f => f.To(It.Is<string>(s => s == recipient)), Times.Once);
            mockFluent.Verify(f => f.Subject(It.Is<string>(s => s == subject)), Times.Once);
            mockFluent.Verify(f => f.Body(It.Is<string>(b => b.Contains(token)), true), Times.Once);
            mockFluent.Verify(f => f.SendAsync(), Times.Once);
        }

    }
}
