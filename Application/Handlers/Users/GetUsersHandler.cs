using Application.Dtos.Users;
using Application.Mappers;
using Application.Queries.Users;
using Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Users
{
    public class GetUsersHandler(
        IUser _usersService
        ) : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUser _usersService = _usersService;
        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await  _usersService.GetAllUsers();
            return [.. users.Select(u => u.MapToUserDto())];
        }
    }
}
