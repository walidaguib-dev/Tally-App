using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface IUserProfile
    {
        public Task<UserProfile> CreateProfile(UserProfile profile);
        public Task<UserProfile?> GetProfileByUserId(string userId);
        public Task<UserProfile?> UpdateProfile(string userId , string Firstname , string Lastname , string? Bio,int uploadId);
    }
}
