using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using Application.Commands;
using Application.Dtos.Users;
using Domain.Contracts;
using Domain.Entities;
using Application.Handlers;

namespace Tests.ApplicationTests.Handlers
{
    public class RegisterUserHandlerTests
    {
        //[Fact]
        //public async Task Handle_ValidRequest_CreatesAndReturnsUser()
        //{
        //    // Arrange
        //    var dto = new RegisterUserDto
        //    {
        //        email = "test@example.com",
        //        username = "testuser",
        //        password = "P@ssw0rd",
        //        Roles = new List<string> { "User" }
        //    };

        //    var mockRepo = new Mock<IUser>();
        //    var mockValidator = new Mock<IValidator<RegisterUserDto>>();

        //    // Validator returns success (no failures)
        //    mockValidator
        //        .Setup(v => v.ValidateAsync(It.IsAny<RegisterUserDto>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(new ValidationResult());

        //    // Capture the user passed to CreateUser and return it (simulate repository behavior)
        //    mockRepo
        //        .Setup(r => r.CreateUser(It.IsAny<User>(), dto.password, dto.Roles))
        //        .ReturnsAsync((User u, string pw, List<string> roles) =>
        //        {
        //            // simulate repository assigning an Id
        //            u.Id = "generated-id";
        //            return u;
        //        });

        //    var handler = new RegisterUserHandler(mockRepo.Object, mockValidator.Object);
        //    var command = new RegisterUserCommand(dto);

        //    // Act
        //    var result = await handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    //As.NotNull(result);
        //    result.Should("generated-id", result.Id);
        //    Assert.Equal(dto.username, result.UserName); // IdentityUser.UserName
        //    mockRepo.Verify(r => r.CreateUser(It.IsAny<User>(), dto.password, dto.Roles), Times.Once);
        //    mockValidator.Verify(v => v.ValidateAsync(It.IsAny<RegisterUserDto>(), It.IsAny<CancellationToken>()), Times.Once);
        //}

    }
}