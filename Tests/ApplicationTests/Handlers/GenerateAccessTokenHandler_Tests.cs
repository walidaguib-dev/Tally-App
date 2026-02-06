using Application.Commands.Tokens;
using Application.Dtos.Tokens;
using Application.Handlers.Tokens;
using Domain.Contracts;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.ApplicationTests.Handlers
{
    public class GenerateAccessTokenHandler_Tests
    {
        [Fact]
        public async Task Handle_ValidRequest_CallsServiceAndReturnsToken()
        {
            // Arrange
            var requestDto = new RefreshTokenRequest
            {
                userId = "user-1",
                refreshTokenString = "refresh-token"
            };

            var mockTokens = new Mock<ITokens>();
            mockTokens
                .Setup(s => s.GenerateAccessToken(requestDto.userId, requestDto.refreshTokenString))
                .ReturnsAsync("jwt-token");

            var mockValidator = new Mock<IValidator<RefreshTokenRequest>>();
            // successful validation
            mockValidator
                .Setup(v => v.ValidateAsync(It.IsAny<RefreshTokenRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var handler = new GenerateAccessTokenHandler(mockTokens.Object, mockValidator.Object);
            var command = new GenerateAccessTokenCommand(requestDto);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("jwt-token", result);
            mockValidator.Verify(v => v.ValidateAsync(It.Is<RefreshTokenRequest>(r => r == requestDto), It.IsAny<CancellationToken>()), Times.Once);
            mockTokens.Verify(s => s.GenerateAccessToken(requestDto.userId, requestDto.refreshTokenString), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ThrowsValidationException_AndDoesNotCallService()
        {
            // Arrange
            var requestDto = new RefreshTokenRequest
            {
                userId = "user-2",
                refreshTokenString = "bad-token"
            };

            var mockTokens = new Mock<ITokens>();

            var mockValidator = new Mock<IValidator<RefreshTokenRequest>>();
            var failures = new[] { new ValidationFailure("refreshTokenString", "invalid") };
            mockValidator
                .Setup(v => v.ValidateAsync(It.IsAny<RefreshTokenRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(failures));

            var handler = new GenerateAccessTokenHandler(mockTokens.Object, mockValidator.Object);
            var command = new GenerateAccessTokenCommand(requestDto);

            // Act & Assert
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => handler.Handle(command, CancellationToken.None));
            mockValidator.Verify(v => v.ValidateAsync(It.Is<RefreshTokenRequest>(r => r == requestDto), It.IsAny<CancellationToken>()), Times.Once);
            mockTokens.Verify(s => s.GenerateAccessToken(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
