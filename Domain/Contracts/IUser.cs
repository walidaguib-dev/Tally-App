using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;


namespace Domain.Contracts
{
    public interface IUser
    {
        public Task<User> CreateUser(User user , string password, List<string> roles);
        public Task<User?> SignIn(string username , string password);
        public Task<User?> ResetPassword(User user , string password);
        public Task<User> VerifyAccount(User user);
        public Task<List<User>> GetAllUsers();

    }
}