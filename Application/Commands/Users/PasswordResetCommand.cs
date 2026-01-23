using Application.Dtos.Users;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Users
{
    public record PasswordResetCommand(PasswordResetDto Dto) : IRequest<User?>;
    
}
