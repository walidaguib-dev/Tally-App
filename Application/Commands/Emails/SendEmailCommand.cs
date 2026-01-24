using Application.Dtos.Mail;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Emails
{
    public record SendEmailCommand(SendEmailDto Dto) : IRequest<EmailToken>;

}
