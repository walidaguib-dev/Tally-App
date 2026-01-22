using Application.Dtos.Tokens;
using Application.Dtos.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Users
{
    public record SignInCommand(SignInDto Dto) : IRequest<LoginResponse>;
}
