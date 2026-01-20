using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tests.InfrastructureTests.Users
{
    public class UserCreationRepositoryTests
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

        [Fact]
        public async Task CreateUser_NoRoles_CallsCreateAsync_AndReturnsSameUser()
        {
            // Arrange
            var mockUserManager = CreateMockUserManager();
            mockUserManager
                .Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var repo = new UsersRepository(mockUserManager.Object);

            var user = new User { UserName = "alice", Email = "alice@example.com" };
            var password = "P@ssw0rd";
            var roles = new List<string>(); // empty

            // Act
            var result = await repo.CreateUser(user, password, roles);

            // Assert
            Assert.Same(user, result);
            mockUserManager.Verify(m => m.CreateAsync(It.Is<User>(u => u == user), password), Times.Once);
            mockUserManager.Verify(m => m.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()), Times.Never);
        }

        [Fact]
        public async Task CreateUser_WithRoles_CallsCreateAsync_ThenAddToRolesAsync()
        {
            var mockUserManager = CreateMockUserManager();
            mockUserManager
                .Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            mockUserManager
                .Setup(m => m.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(IdentityResult.Success);
            var repo = new UsersRepository(mockUserManager.Object);

            var user = new User { UserName = "bob", Email = "bob@example.com" };
            var password = "S3cur3!";
            var roles = new List<string> { "Admin", "User" };

            // Act
            var result = await repo.CreateUser(user, password, roles);

            // Assert
            Assert.Same(user, result);
            mockUserManager.Verify(m => m.CreateAsync(It.Is<User>(u => u == user), password), Times.Once);
            mockUserManager.Verify(m => m.AddToRolesAsync(It.Is<User>(u => u == user), It.Is<IEnumerable<string>>(r => r == roles)), Times.Once);
        }

        [Fact]
        public async Task CreateUser_AddToRolesFails_ThrowsValidationExceptionWithErrors()
        {
            var mockUserManager = CreateMockUserManager();
            mockUserManager
                .Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var identityErrors = new[] { new IdentityError { Description = "Role creation failed" } };
            var failedResult = IdentityResult.Failed(identityErrors);

            mockUserManager
                .Setup(m => m.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(failedResult);
            var repo = new UsersRepository(mockUserManager.Object);

            var user = new User { UserName = "charlie", Email = "charlie@example.com" };
            var password = "AnotherP@ss";
            var roles = new List<string> { "Manager" };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => repo.CreateUser(user, password, roles));
            Assert.Contains("Role creation failed", ex.Message);

            mockUserManager.Verify(m => m.CreateAsync(It.Is<User>(u => u == user), password), Times.Once);
            mockUserManager.Verify(m => m.AddToRolesAsync(It.Is<User>(u => u == user), It.Is<IEnumerable<string>>(r => r == roles)), Times.Once);
        }
    }
}
