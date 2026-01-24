using Application.Dtos.Mail;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Emails
{
    public class SendEmailValidation : AbstractValidator<SendEmailDto>
    {
        public SendEmailValidation()
        {
            RuleFor(x => x.Purpose).IsInEnum().WithMessage("Invalid verification purpose.");
            RuleFor(x => x.userId)
                .NotEmpty()
                .NotNull()
                .WithMessage("user id is required");
        }
    }
}
