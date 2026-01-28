using Application.Commands.Users.Profiles;
using Application.Dtos.Users.Profiles;
using Application.Mappers;
using Domain.Contracts;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Users.Profiles
{
    internal class CreateUserProfileHandler(
        IUserProfile userProfileService,
        [FromKeyedServices("CreateUserProfile")] IValidator<CreateUserProfileDto> validator
        ) : IRequestHandler<CreateUserProfileCommand, UserProfile>
    {
        private readonly IUserProfile _userProfileService = userProfileService;
        private readonly IValidator<CreateUserProfileDto> _validator = validator;
        public async Task<UserProfile> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var entity = request.Dto.MapToEntity();
            var createdProfile = await _userProfileService.CreateProfile(entity);
            return createdProfile;
        }
    }
}
