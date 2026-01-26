using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;


namespace Domain.Contracts
{
    public interface IUser
    {
        public Task<User> CreateUser(User user , string password,string role);
        public Task<User?> SignIn(string username , string password);
        public Task<User?> ResetPassword(string userId, string current_password, string new_password);
        public Task<User?> ForgetPasswordReset(string userId , string token , string new_password);
        public Task<List<User>> GetAllUsers();

    }
}