using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Emails;
using Application.Dtos.Mail;
using Domain.Contracts;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using API.Controllers;

namespace Tests.ApiTests.Controllers;

public class EmailsControllerTests
{
    [Fact]
    public async Task SendEmail_ReturnsOk_WhenMediatorReturnsToken()
    {
        // Arrange
        var dto = new SendEmailDto { userId = "u1", Purpose = Domain.Enums.VerificationPurpose.EmailVerification };
        var token = new EmailToken { UserId = dto.userId, CodeHash = "abc", Purpose = dto.Purpose, User = new User { UserName = "u1" } };

        var mockMediator = new Mock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.IsAny<SendEmailCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(token);

        var mockEmailsService = new Mock<IEmail>();

        var controller = new EmailsController(mockMediator.Object, mockEmailsService.Object);

        // Act
        var result = await controller.SendEmail(dto);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(ok.Value);
        mockMediator.Verify(m => m.Send(It.Is<SendEmailCommand>(c => c.Dto == dto), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SendEmail_ReturnsUnauthorized_WhenMediatorReturnsNull()
    {
        // Arrange
        var dto = new SendEmailDto { userId = "u2", Purpose = Domain.Enums.VerificationPurpose.EmailVerification };

        var mockMediator = new Mock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.IsAny<SendEmailCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((EmailToken?)null);

        var mockEmailsService = new Mock<IEmail>();
        var controller = new EmailsController(mockMediator.Object, mockEmailsService.Object);

        // Act
        var result = await controller.SendEmail(dto);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
        mockMediator.Verify(m => m.Send(It.Is<SendEmailCommand>(c => c.Dto == dto), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SendEmail_ReturnsBadRequest_WithValidationErrors_WhenMediatorThrowsValidationException()
    {
        // Arrange
        var dto = new SendEmailDto { userId = "u3", Purpose = Domain.Enums.VerificationPurpose.EmailVerification };

        var mockMediator = new Mock<IMediator>();
        var failures = new[] { new ValidationFailure("userId", "required") };
        mockMediator
            .Setup(m => m.Send(It.IsAny<SendEmailCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ValidationException(failures));

        var mockEmailsService = new Mock<IEmail>();
        var controller = new EmailsController(mockMediator.Object, mockEmailsService.Object);

        // Act
        var result = await controller.SendEmail(dto);

        // Assert
        var bad = Assert.IsType<BadRequestObjectResult>(result);
        var payload = bad.Value as dynamic;
        Assert.NotNull(payload);
        var errors = ((System.Collections.IEnumerable)payload.Errors!).Cast<object>().Select(o => o.ToString()).ToList();
        Assert.Contains("required", string.Join(" ", errors));
    }

    [Fact]
    public async Task ConfirmEmail_ReturnsBadRequest_WhenServiceReturnsNull()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        var mockEmailsService = new Mock<IEmail>();
        mockEmailsService.Setup(s => s.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((object?)null);

        var controller = new EmailsController(mockMediator.Object, mockEmailsService.Object);

        // Act
        var result = await controller.ConfirmEmail("u1", "tok");

        // Assert
        var bad = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("user not found!", bad.Value);
    }

    [Fact]
    public async Task ConfirmEmail_ReturnsRedirect_WhenServiceReturnsDone()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        var mockEmailsService = new Mock<IEmail>();
        mockEmailsService.Setup(s => s.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((object?)"Done");

        var controller = new EmailsController(mockMediator.Object, mockEmailsService.Object);

        // Act
        var result = await controller.ConfirmEmail("u1", "tok");

        // Assert
        var redirect = Assert.IsType<RedirectResult>(result);
        Assert.Equal("https://fb.com", redirect.Url);
    }

    [Fact]
    public async Task ConfirmEmail_ReturnsBadRequest_WithExceptionMessage_WhenServiceThrows()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        var mockEmailsService = new Mock<IEmail>();
        mockEmailsService.Setup(s => s.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new Exception("boom"));

        var controller = new EmailsController(mockMediator.Object, mockEmailsService.Object);

        // Act
        var result = await controller.ConfirmEmail("u1", "tok");

        // Assert
        var bad = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("boom", bad.Value);
    }
}