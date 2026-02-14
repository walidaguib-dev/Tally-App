using Application.Commands.Tokens;
using Application.Dtos.Tokens;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Tokens
{
    public class RefreshTokenRequestValidation : AbstractValidator<GenerateAccessTokenCommand>
    {
        public RefreshTokenRequestValidation()
        {
            RuleFor(x => x.TokenRequest.userId)
               .NotEmpty()
               .NotNull()
               .WithMessage("the user id is required!")
               .WithName("User Id");

            RuleFor(x => x.TokenRequest.refreshTokenString)
               .NotEmpty()
               .NotNull()
               .WithMessage("the user id is required!")
               .WithName("Refresh token");
        }
    }
}
