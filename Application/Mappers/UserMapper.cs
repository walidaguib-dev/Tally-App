using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public static class UserMapper
    {
        public static User MapToUser(this Application.Dtos.Users.RegisterUserDto dto)
        {
            return new User
            {
                UserName = dto.username,
                Email = dto.email,
            };
        }

        public static Application.Dtos.Users.UserDto MapToUserDto(this User user)
        {
            return new Application.Dtos.Users.UserDto
            {
                Id = user.Id,
                email = user.Email!,
                username = user.UserName!,
                email_confirmed = user.EmailConfirmed
            };
        }
    }
}
