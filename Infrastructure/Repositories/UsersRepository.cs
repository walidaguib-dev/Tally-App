using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UsersRepository
        (
        UserManager<User> userManager,
        ApplicationDbContext context,
        SignInManager<User> signInManager
        ) : IUser
    {
        private readonly UserManager<User> _userManager = userManager;
       
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly ApplicationDbContext _context = context;
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

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public Task<User?> ResetPassword(User user, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> SignIn(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return null; // do NOT reveal whether user exists
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(
                user,
                password,
                lockoutOnFailure: true
            );

            if (!signInResult.Succeeded)
            {
                return null;
            }

            return user;


        }

        public Task<User> VerifyAccount(User user)
        {
            throw new NotImplementedException();
        }
    }
}
