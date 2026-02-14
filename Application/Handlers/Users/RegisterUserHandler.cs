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
         IEmail emailsService
        ) : IRequestHandler<RegisterUserCommand, User>
    {
        private readonly IUser _userRepository = repo;
        private readonly IEmail _emailsService = emailsService;

        public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
                var user = request.Dto.MapToUser();
                await _userRepository.CreateUser(user,request.Dto.password , request.Dto.Role);
                await _emailsService.SendEmail(user.Id, Domain.Enums.VerificationPurpose.EmailVerification);
                return user;
        }
    }
}
