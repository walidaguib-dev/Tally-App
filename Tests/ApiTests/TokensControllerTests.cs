using API.Controllers;
using Application.Commands.Tokens;
using Application.Dtos.Tokens;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.ApiTests
{
    public class TokensControllerTests
    {

        [Fact]
        public async Task GenerateToken_ReturnsOk_WithToken()
        {
            // Arrange
            var dto = new RefreshTokenRequest
            {
                userId = "user-1",
                refreshTokenString = "refresh-xyz"
            };

            var expectedToken = "jwt-token-123";

            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(m => m.Send(It.IsAny<GenerateAccessTokenCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedToken);

            var controller = new TokensController(mockMediator.Object);

            // Act
            var actionResult = await controller.GenerateToken(dto);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(expectedToken, ok.Value);
        }

        [Fact]
        public async Task GenerateToken_InvokesMediatorSend_WithGenerateAccessTokenCommand()
        {
            // Arrange
            var dto = new RefreshTokenRequest
            {
                userId = "user-2",
                refreshTokenString = "refresh-abc"
            };

            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(m => m.Send(It.Is<GenerateAccessTokenCommand>(c => c.TokenRequest == dto), It.IsAny<CancellationToken>()))
                .ReturnsAsync("any-token");

            var controller = new TokensController(mockMediator.Object);

            // Act
            var _ = await controller.GenerateToken(dto);

            // Assert
            mockMediator.Verify(m => m.Send(It.Is<GenerateAccessTokenCommand>(c => c.TokenRequest == dto), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
