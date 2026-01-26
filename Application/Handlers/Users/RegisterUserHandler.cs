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
using Application.Commands.Users;

namespace Application.Handlers.Users
{
    public class RegisterUserHandler
        (IUser repo ,
         [FromKeyedServices("Register")] IValidator<RegisterUserDto> validator,
         IEmail emailsService
        ) : IRequestHandler<RegisterUserCommand, User>
    {
        private readonly IUser _userRepository = repo;
        private readonly IValidator<RegisterUserDto> _validator = validator;
        private readonly IEmail _emailsService = emailsService;

        public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            ValidationResult? result = await _validator.ValidateAsync(request.Dto, cancellationToken);
            if(result.IsValid)
            {
                var user = request.Dto.MapToUser();
                await _userRepository.CreateUser(user,request.Dto.password , request.Dto.Role);
                await _emailsService.SendEmail(user.Id, Domain.Enums.VerificationPurpose.EmailVerification);
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
