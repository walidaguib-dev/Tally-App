using Application.Dtos.Tokens;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Tokens
{
    public class RefreshTokenRequestValidation : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidation()
        {
            RuleFor(x => x.userId)
               .NotEmpty()
               .NotNull()
               .WithMessage("the user id is required!");

            RuleFor(x => x.refreshTokenString)
               .NotEmpty()
               .NotNull()
               .WithMessage("the user id is required!");
        }
    }
}
