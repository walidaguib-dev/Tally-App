using Application.Commands.Emails;
using Application.Dtos.Mail;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Emails
{
    public class SendEmailHandler(
        IEmail emailsService,
        [FromKeyedServices("EmailValidator")] IValidator<SendEmailDto> validator
        ) : IRequestHandler<SendEmailCommand, EmailToken>
    {
        private readonly IEmail EmailsService = emailsService;
        private readonly IValidator<SendEmailDto> Validator = validator;
        public async Task<EmailToken> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await Validator.ValidateAsync(request.Dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var userId = request.Dto.userId;
            var purpose = request.Dto.Purpose;
            var result = await EmailsService.SendEmail(userId , purpose);
            if(result == null)
            {
                throw new Exception("Failed to send email.");
            }
            return result;
        }
    }
}
