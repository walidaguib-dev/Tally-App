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
        IUser usersService,
        [FromKeyedServices("ForgetPasswordReset")] IValidator<ForgetPasswordDto> validator
        ) : IRequestHandler<ForgetPasswordCommand, UserDto>
    {
        private readonly IUser _usersService = usersService;
        private readonly IValidator<ForgetPasswordDto> _validator = validator;
        public async Task<UserDto> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.Dto,cancellationToken);
            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var userId = request.Dto.userId;
            var newPassword = request.Dto.new_password;
            var token = request.Dto.Token;
            var result = await _usersService.ForgetPasswordReset(userId,token,newPassword) ?? throw new Exception("Password reset failed.");
            return result.MapToUserDto();
        }
    }
}
