using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.Teams;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TeamsRepository(ApplicationDbContext _context, ICaching _cachingService) : ITeams
    {
        private readonly ApplicationDbContext context = _context;
        private readonly ICaching cachingService = _cachingService;

        public async Task<TeamMember> AddMember(TeamMember member)
        {
            await context.TeamMembers.AddAsync(member);
            await context.SaveChangesAsync();
            return member;
        }

        public async Task<Team> CreateTeam(Team team)
        {
            await context.Teams.AddAsync(team);
            await context.SaveChangesAsync();
            return team;
        }

        public async Task<bool?> DeleteTeam(int id)
        {
            var affectedRow = await context.Teams.Where(x => x.Id == id).ExecuteDeleteAsync();
            return affectedRow == 0 ? null : true;
        }

        public async Task<List<Team>> GetAll()
        {
            string key = "teams";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    return await context
                        .Teams.AsNoTracking()
                        .AsSplitQuery()
                        .Include(x => x.Supervisor)
                        .Include(x => x.Members)
                        .ToListAsync(token);
                },
                TimeSpan.FromMinutes(20),
                ["teams"]
            );
            return result ?? [];
        }

        public async Task<Team?> GetById(int id)
        {
            string key = $"team_{id}";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    return await context
                        .Teams.AsNoTracking()
                        .AsSplitQuery()
                        .Include(x => x.Supervisor)
                        .Include(x => x.Members)
                        .FirstOrDefaultAsync(x => x.Id == id, token);
                },
                TimeSpan.FromMinutes(10),
                ["teams"]
            );

            return result;
        }

        public async Task<List<TeamMember>> GetMembers(int teamId)
        {
            string key = $"members_{teamId}";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    return await context
                        .TeamMembers.Where(x => x.TeamId == teamId)
                        .AsNoTracking()
                        .AsSplitQuery()
                        .Include(x => x.User)
                        .ToListAsync(token);
                },
                TimeSpan.FromMinutes(10),
                ["members"]
            );

            return result ?? [];
        }

        public async Task<bool?> RemoveMember(int teamId, string userId)
        {
            var affectedRow = await context
                .TeamMembers.Where(x => x.TeamId == teamId && x.UserId == userId)
                .ExecuteDeleteAsync();
            return affectedRow == 0 ? null : true;
        }

        public async Task<bool?> UpdateTeam(int id, string name, string? description)
        {
            var affectedRow = await context
                .Teams.Where(x => x.Id == id)
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(p => p.Name, name).SetProperty(p => p.Description, description)
                );
            return affectedRow == 0 ? null : true;
        }
    }
}
