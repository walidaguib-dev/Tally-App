using Application.Commands;
using Application.Dtos.Users;
using Application.Mappers;
using Domain.Entities;
using FluentValidation;
using Domain.Contracts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;

namespace Application.Handlers
{
    public class RegisterUserHandler
        (IUser repo ,
         [FromKeyedServices("Register")] IValidator<RegisterUserDto> validator
        ) : IRequestHandler<RegisterUserCommand, User>
    {
        private readonly IUser _userRepository = repo;
        private readonly IValidator<RegisterUserDto> _validator = validator;   

        public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            ValidationResult? result = await _validator.ValidateAsync(request.Dto);
            if(result.IsValid)
            {
                var user = request.Dto.MapToUser();
                await _userRepository.CreateUser(user,request.Dto.password , request.Dto.Roles);
                return user;
            }
            else
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
        }
    }
}
