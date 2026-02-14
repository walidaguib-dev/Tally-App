using Application.Commands.Users;
using Application.Dtos.Users;
using Application.Mappers;
using Domain.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Users
{
    internal class ForgetPasswordHandler(
        IUser usersService

        ) : IRequestHandler<ForgetPasswordCommand, UserDto>
    {
        private readonly IUser _usersService = usersService;

        public async Task<UserDto> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
           
            var userId = request.Dto.userId;
            var newPassword = request.Dto.new_password;
            var token = request.Dto.Token;
            var result = await _usersService.ForgetPasswordReset(userId,token,newPassword) ?? throw new Exception("Password reset failed.");
            return result.MapToUserDto();
        }
    }
}
