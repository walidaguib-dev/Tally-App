using Application.Commands.Tokens;
using Application.Dtos.Tokens;
using Domain.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Tokens
{
    public class GenerateAccessTokenHandler(
          ITokens tokensService,
          [FromKeyedServices("GenerateToken")] IValidator<RefreshTokenRequest> validator
        ) : IRequestHandler<GenerateAccessTokenCommand, string?>
    {
        private readonly ITokens _tokensService = tokensService;
        private readonly IValidator<RefreshTokenRequest> _validator = validator;

        public async Task<string?> Handle(GenerateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var ValidationResult = await _validator.ValidateAsync(request.TokenRequest);
            if (!ValidationResult.IsValid)
            {
                throw new ValidationException(ValidationResult.Errors);
            }

            var result = await _tokensService.GenerateAccessToken(userId: request.TokenRequest.userId, token: request.TokenRequest.refreshTokenString);
            return result;
        }
    }
}
