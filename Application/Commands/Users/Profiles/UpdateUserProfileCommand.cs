using Application.Dtos.Users.Profiles;
using Domain.Entities;
using Domain.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Users.Profiles
{
    public record UpdateUserProfileCommand(UpdateUserProfileDto Dto, string userId) : IRequest<UserProfile>, IInvalidateCache
    {
        public List<string> CacheKeys => ["profiles", $"profile_{userId}"]; 
    }
}
