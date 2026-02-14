using Application.Commands.Users.Profiles;
using Application.Dtos.Users.Profiles;
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
    public class UpdateUserProfileHandler(
        IUserProfile userProfileService
        ) : IRequestHandler<UpdateUserProfileCommand, UserProfile>
    {
        private readonly IUserProfile _userProfileService = userProfileService;
        public async Task<UserProfile> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            
            var firstname = request.Dto.FirstName!.Trim();
            var lastname = request.Dto.LastName!.Trim();
            var bio = request.Dto.Bio?.Trim();
            var updatedProfile = await _userProfileService.UpdateProfile(request.userId,firstname,lastname,bio) ?? throw new Exception("Failed to update user profile.");
            return updatedProfile;
        }
    }
}
