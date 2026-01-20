using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UsersRepository
        (
        UserManager<User> userManager
        ) : IUser
    {
        private readonly UserManager<User> _userManager = userManager;
        public async Task<User> CreateUser(User user , string password , List<string> roles)
        {
            var result = await _userManager.CreateAsync(user , password);
            if (roles != null && roles.Any())
            {
                var roleResult = await _userManager.AddToRolesAsync(user, roles);

                if (!roleResult.Succeeded)
                {
                    throw new ValidationException(string.Join(", ",
                        roleResult.Errors.Select(e => e.Description)));
                }
            }
            return user;
        }

        public Task<User?> ResetPassword(User user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User?> SignIn(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> VerifyAccount(User user)
        {
            throw new NotImplementedException();
        }
    }
}
