using Application.Commands;
using Application.Dtos.Users;
using Application.Handlers;
using Application.Mappers;
using Domain.Contracts;
using Domain.Entities;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ApplicationTests.Handlers
{
    public class RegisterUserHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_CreatesAndReturnsUser()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                email = "test@example.com",
                username = "testuser",
                password = "P@ssw0rd",
                Roles = new List<string> { "User" }
            };

            var mockRepo = new Mock<IUser>();
            var mockValidator = new Mock<IValidator<RegisterUserDto>>();

            // Validator returns success (no failures)
            mockValidator
                .Setup(v => v.ValidateAsync(It.IsAny<RegisterUserDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            // Capture the user passed to CreateUser and return it (simulate repository behavior)
            mockRepo
                .Setup(r => r.CreateUser(It.IsAny<User>(), dto.password, dto.Roles))
                .ReturnsAsync((User u, string pw, List<string> roles) =>
                {
                    // simulate repository assigning an Id
                    u.Id = "generated-id";
                    return u;
                });

            var handler = new RegisterUserHandler(mockRepo.Object, mockValidator.Object);
            var command = new RegisterUserCommand(dto);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be("generated-id");
            result.UserName.Should().Be("testuser"); // IdentityUser.UserName
            mockRepo.Verify(r => r.CreateUser(It.IsAny<User>(), dto.password, dto.Roles), Times.Once);
            mockValidator.Verify(v => v.ValidateAsync(It.IsAny<RegisterUserDto>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                email = "bad@example.com",
                username = "",
                password = "short",
                Roles = new List<string>()
            };

            var mockRepo = new Mock<IUser>();
            var mockValidator = new Mock<IValidator<RegisterUserDto>>();

            // Validator returns a failure
            var failures = new List<ValidationFailure> { new ValidationFailure("username", "Username is required") };
            mockValidator
                .Setup(v => v.ValidateAsync(It.IsAny<RegisterUserDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(failures));

            var handler = new RegisterUserHandler(mockRepo.Object, mockValidator.Object);
            var command = new RegisterUserCommand(dto);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));

            // Ensure repository was never called
            mockRepo.Verify(r => r.CreateUser(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<List<string>>()), Times.Never);
            mockValidator.Verify(v => v.ValidateAsync(It.IsAny<RegisterUserDto>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}


