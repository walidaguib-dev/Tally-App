using Application.Commands.Emails;
using Application.Dtos.Mail;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Emails
{
    public class SendEmailValidation : AbstractValidator<SendEmailCommand>
    {
        public SendEmailValidation()
        {
            RuleFor(x => x.Dto.Purpose).IsInEnum().WithMessage("Invalid verification purpose.").WithName("Email Purpose");
            RuleFor(x => x.Dto.userId)
                .NotEmpty()
                .NotNull()
                .WithMessage("user id is required").WithName("User Id");
        }
    }
}
