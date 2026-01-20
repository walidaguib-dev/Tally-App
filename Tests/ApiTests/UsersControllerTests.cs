using API.Controllers;
using Application.Commands;
using Application.Dtos.Users;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.ApiTests
{
    public class UsersControllerTests
    {
        [Fact]
        public async Task RegisterUser_ReturnsCreatedAtAction_WithRouteIdAndValue()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                email = "user@example.com",
                username = "testuser",
                password = "P@ssw0rd",
                Roles = new System.Collections.Generic.List<string> { "User" }
            };


            var returnedUser = new User
            {
                Id = "generated-id",
                UserName = dto.username,
                Email = dto.email
            };

            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedUser);

            var controller = new UsersController(mockMediator.Object);

            // Act
            var actionResult = await controller.RegisterUser(dto);

            var created = Assert.IsType<CreatedAtActionResult>(actionResult);
            Assert.Equal(nameof(UsersController.RegisterUser), created.ActionName);
            Assert.NotNull(created.RouteValues);
            Assert.True(created.RouteValues.ContainsKey("id"));
            Assert.Equal(returnedUser.Id, created.RouteValues["id"]);
            Assert.NotNull(created.Value); // mapped DTO returned as body
        }

        [Fact]
        public async Task RegisterUser_InvokesMediatorSend_WithRegisterUserCommand()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                email = "invoker@example.com",
                username = "invoker",
                password = "InvokerP@ss",
                Roles = new System.Collections.Generic.List<string>()
            };

            var returnedUser = new User { Id = "id-123", UserName = dto.username, Email = dto.email };

            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(m => m.Send(It.Is<RegisterUserCommand>(c => c.Dto == dto), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedUser);

            var controller = new UsersController(mockMediator.Object);
            // Act
            var _ = await controller.RegisterUser(dto);

            // Assert
            mockMediator.Verify(m => m.Send(It.Is<RegisterUserCommand>(c => c.Dto == dto), It.IsAny<CancellationToken>()), Times.Once);

        }
    }
}
