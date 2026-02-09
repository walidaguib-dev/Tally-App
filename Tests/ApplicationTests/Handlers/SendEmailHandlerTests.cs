using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using FluentValidation;
using FluentValidation.Results;
using Application.Handlers.Emails;
using Application.Commands.Emails;
using Application.Dtos.Mail;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using System;

namespace Tests.ApplicationTests.Handlers;

public class SendEmailHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_CallsServiceAndReturnsToken()
    {
        // Arrange
        var dto = new SendEmailDto { userId = "u1", Purpose = VerificationPurpose.EmailVerification };
        var expected = new EmailToken { UserId = dto.userId, CodeHash = "code", Purpose = dto.Purpose };

        var mockEmailService = new Mock<IEmail>();
        mockEmailService.Setup(s => s.SendEmail(dto.userId, dto.Purpose)).ReturnsAsync(expected);

        var mockValidator = new Mock<IValidator<SendEmailDto>>();
        mockValidator.Setup(v => v.ValidateAsync(It.IsAny<SendEmailDto>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new ValidationResult());

        var handler = new SendEmailHandler(mockEmailService.Object, mockValidator.Object);
        var command = new SendEmailCommand(dto);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(expected, result);
        mockEmailService.Verify(s => s.SendEmail(dto.userId, dto.Purpose), Times.Once);
        mockValidator.Verify(v => v.ValidateAsync(It.IsAny<SendEmailDto>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ThrowsValidationException_AndDoesNotCallService()
    {
        // Arrange
        var dto = new SendEmailDto { userId = "u2", Purpose = VerificationPurpose.PasswordReset };

        var mockEmailService = new Mock<IEmail>();

        var mockValidator = new Mock<IValidator<SendEmailDto>>();
        var failures = new[] { new ValidationFailure("userId", "required") };
        mockValidator.Setup(v => v.ValidateAsync(It.IsAny<SendEmailDto>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new ValidationResult(failures));

        var handler = new SendEmailHandler(mockEmailService.Object, mockValidator.Object);
        var command = new SendEmailCommand(dto);

        // Act & Assert
        await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => handler.Handle(command, CancellationToken.None));
        mockEmailService.Verify(s => s.SendEmail(It.IsAny<string>(), It.IsAny<VerificationPurpose>()), Times.Never);
        mockValidator.Verify(v => v.ValidateAsync(It.IsAny<SendEmailDto>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ServiceReturnsNull_ThrowsException()
    {
        // Arrange
        var dto = new SendEmailDto { userId = "u3", Purpose = VerificationPurpose.EmailVerification };

        var mockEmailService = new Mock<IEmail>();
        mockEmailService.Setup(s => s.SendEmail(dto.userId, dto.Purpose)).ReturnsAsync((EmailToken?)null);

        var mockValidator = new Mock<IValidator<SendEmailDto>>();
        mockValidator.Setup(v => v.ValidateAsync(It.IsAny<SendEmailDto>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new ValidationResult());

        var handler = new SendEmailHandler(mockEmailService.Object, mockValidator.Object);
        var command = new SendEmailCommand(dto);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        mockEmailService.Verify(s => s.SendEmail(dto.userId, dto.Purpose), Times.Once);
    }
}