using Application.Dtos.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Users
{
    public record GetAllUsersQuery : IRequest<List<UserDto>>;
   
}
