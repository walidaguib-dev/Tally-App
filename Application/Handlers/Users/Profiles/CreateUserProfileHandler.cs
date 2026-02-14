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
        IUserProfile userProfileService
        ) : IRequestHandler<CreateUserProfileCommand, UserProfile>
    {
        private readonly IUserProfile _userProfileService = userProfileService;
        public async Task<UserProfile> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Dto.MapToEntity();
            var createdProfile = await _userProfileService.CreateProfile(entity);
            return createdProfile;
        }
    }
}
