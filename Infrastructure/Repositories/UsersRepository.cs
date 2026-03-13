using Domain.Contracts;
using Domain.Entities;
using Domain.helpers;
using FluentValidation;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UsersRepository
        (
        UserManager<User> userManager,

        SignInManager<User> signInManager
        ) : IUser
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        public async Task<User> CreateUser(User user, string password, string role)
        {
            var userExists = await _userManager.FindByNameAsync(user.UserName!);

            if (userExists != null)
                throw new InvalidOperationException("User already exists!");

            var result = await _userManager.CreateAsync(user, password);

            var roleResult = await _userManager.AddToRoleAsync(user, role);

            if (!roleResult.Succeeded)
            {
                throw new Exception(string.Join(", ",
                    roleResult.Errors.Select(e => e.Description)));
            }



            return user;
        }

        public async Task<User?> ForgetPasswordReset(string userId, string token, string new_password)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("User not found!");

            var result = await _userManager.ResetPasswordAsync(user, token, new_password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ",
                    result.Errors.Select(e => e.Description)));
            }
            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userManager.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> ResetPassword(string userId, string current_password, string new_password)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return null;

            var passwordIsMatch = await _userManager.CheckPasswordAsync(user, current_password);
            if (!passwordIsMatch)
                throw new Exception("Password not match!");

            var result = await _userManager.ChangePasswordAsync(user, current_password, new_password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ",
                    result.Errors.Select(e => e.Description)));
            }

            return user;
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

            if (signInResult.IsLockedOut)
                throw new AuthenticationException("Account locked due to failed attempts.");

            if (signInResult.RequiresTwoFactor)
                throw new AuthenticationException("Two-factor authentication required.");

            return !signInResult.Succeeded ? throw new AuthenticationException("Invalid credentials") : user;
        }


    }
}
