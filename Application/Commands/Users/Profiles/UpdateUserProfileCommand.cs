using Application.Dtos.Users.Profiles;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Users.Profiles
{
    public record UpdateUserProfileCommand(UpdateUserProfileDto Dto , string userId) : IRequest<UserProfile>;
   
}
