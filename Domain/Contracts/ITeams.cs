using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Teams;

namespace Domain.Contracts
{
    public interface ITeams
    {
        Task<Team> CreateTeam(Team team);
        Task<Team?> GetById(int id);
        Task<List<Team>> GetAll();
        Task<bool?> UpdateTeam(int id, string name, string? description);
        Task<bool?> DeleteTeam(int id);

        // Members
        Task<TeamMember> AddMember(TeamMember member);
        Task<bool?> RemoveMember(int teamId, string userId);
        Task<List<TeamMember>> GetMembers(int teamId);
    }
}
