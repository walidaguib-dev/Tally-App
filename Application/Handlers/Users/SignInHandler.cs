using Application.Commands.Users;
using Application.Dtos.Tokens;
using Application.Dtos.Users;
using Domain.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Users
{
    public class SignInHandler(
        IUser usersService,

        ITokens tokensService
        ) : IRequestHandler<SignInCommand, LoginResponse>
    {
        private readonly IUser _usersService = usersService;
 
        private readonly ITokens _tokensService = tokensService;

        public async Task<LoginResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            
            var result = await _usersService.SignIn(request.Dto.username, request.Dto.password);
            var tokenResult = await _tokensService.GenerateRefreshToken(result!);

            RefreshTokenRequest tokenRequest = new()
            {
                refreshTokenString = tokenResult.Token,
                userId = result!.Id
            };

            var accessToken = await tokensService.GenerateAccessToken(tokenRequest.userId,tokenRequest.refreshTokenString);

            return new LoginResponse
            {
                Access_Token = accessToken!,
                Refresh_Token = tokenResult.Token
            };
        }
    }
}
