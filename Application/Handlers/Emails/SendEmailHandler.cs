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
        IEmail emailsService
        ) : IRequestHandler<SendEmailCommand, EmailToken>
    {
        private readonly IEmail EmailsService = emailsService;
        public async Task<EmailToken> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
           
            var userId = request.Dto.userId;
            var purpose = request.Dto.Purpose;
            var result = await EmailsService.SendEmail(userId , purpose);
            return result ?? throw new Exception("Failed to send email.");
        }
    }
}
