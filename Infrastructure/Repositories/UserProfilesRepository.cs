using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZiggyCreatures.Caching.Fusion;

namespace Infrastructure.Repositories
{
    public class UserProfilesRepository(

        ApplicationDbContext context,
        ICaching cachingService
        ) : IUserProfile
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ICaching _cachingService = cachingService;
        public async Task<UserProfile> CreateProfile(UserProfile profile)
        {
            var result = await _context.profiles.AddAsync(profile);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<UserProfile?> GetProfileByUserId(string userId)
        {
            var key = $"profile_{userId}";
            var cachedProfile = await _cachingService.GetOrSetAsync(
                key,
                async token => await _context.profiles
                .Include(p => p.Upload)
                .Include(p => p.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken: token)
                , TimeSpan.FromHours(1));
            if (cachedProfile is null) return null;
            return cachedProfile;
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
            return profile;
        }
    }
}
