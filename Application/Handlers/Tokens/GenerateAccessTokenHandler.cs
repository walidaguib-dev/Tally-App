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
          ITokens tokensService
        ) : IRequestHandler<GenerateAccessTokenCommand, string?>
    {
        private readonly ITokens _tokensService = tokensService;


        public async Task<string?> Handle(GenerateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _tokensService.GenerateAccessToken(userId: request.TokenRequest.userId, token: request.TokenRequest.refreshTokenString);
            return result;
        }
    }
}
