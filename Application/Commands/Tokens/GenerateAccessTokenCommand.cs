using Application.Dtos.Tokens;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Tokens
{
    public record GenerateAccessTokenCommand(RefreshTokenRequest TokenRequest) : IRequest<string?>;
   
}
