using Application.Dtos.Users.Profiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Users.Profiles
{
    public record GetUserProfileQuery(string userId) : IRequest<ProfileDto>;

}
