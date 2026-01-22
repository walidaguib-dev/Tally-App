using Application.Dtos.Users;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Users
{
    public record RegisterUserCommand(RegisterUserDto Dto) : IRequest<User>;
    
}
