using Application.Commands.Users;
using Application.Dtos.Users;
using Domain.Contracts;
using Domain.Entities;
using Domain.helpers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Handlers.Users
{
    public class PasswordResetHandler(
        IUser _usersService,
        [FromKeyedServices("PasswordReset")] IValidator<PasswordResetDto> _validator
        ) : IRequestHandler<PasswordResetCommand, User?>
    {
        private readonly IUser usersService = _usersService;
        private readonly IValidator<PasswordResetDto> validator = _validator;
        public async Task<User?> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
        {
            var validationResult  = await validator.ValidateAsync(request.Dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var userId = request.Dto.userId;
            var newPassword = request.Dto.new_password;
            var currentPassword = request.Dto.current_password;
            var user = await usersService.ResetPassword(userId,currentPassword,newPassword);
            if(user == null)
            {
                return null;
            }
            return user;
        }
    }
}
