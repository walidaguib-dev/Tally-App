using Application.Dtos.Users.Profiles;
using Application.Mappers;
using Application.Queries.Users.Profiles;
using Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Users.Profiles
{
    public class GetUserProfileHandler(
        IUserProfile userProfileService
        ) : IRequestHandler<GetUserProfileQuery, ProfileDto>
    {
        private readonly IUserProfile _userProfileService = userProfileService;
        public async Task<ProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var profile =  await _userProfileService.GetProfileByUserId(request.userId); 
            if(profile == null)
            {
                throw new Exception("Profile not found");
            };

            var result = profile.MapToDto();
            return result;
        }
    }
}
