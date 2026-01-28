using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserProfilesRepository(
        ICaching cachingService,
        ApplicationDbContext context
        ) : IUserProfile
    {
        private readonly ICaching _cachingService = cachingService;
        private readonly ApplicationDbContext _context = context;
        public async Task<UserProfile> CreateProfile(UserProfile profile)
        {
            var result = await _context.profiles.AddAsync(profile);
            await _context.SaveChangesAsync();
            await _cachingService.RemoveByPattern("user_profile");
            return result.Entity;
        }

        public async Task<UserProfile?> GetProfileByUserId(string userId)
        {
            var key = $"user_profile_{userId}";
            var cachedProfile = await _cachingService.GetFromCacheAsync<UserProfile>(key);
            if (cachedProfile != null)
            {
                return cachedProfile;
            }

            var profile = await _context.profiles
                .Include(p => p.Upload)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null) return null;
            await _cachingService.SetAsync(key, profile, TimeSpan.FromHours(1));
            return profile;
        }

        public async Task<UserProfile?> UpdateProfile(string userId, string Firstname, string Lastname, string? Bio)
        {
            var profile = await _context.profiles
                .Include(p => p.Upload)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null) return null;
            profile.FirstName = Firstname;
            profile.LastName = Lastname;
            profile.Bio = Bio;
            profile.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            await _cachingService.RemoveCaching($"user_profile_{userId}");
            return profile;
        }
    }
}
