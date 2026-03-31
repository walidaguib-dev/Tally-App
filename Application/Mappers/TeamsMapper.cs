using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Teams;
using Application.Dtos.Teams.TeamMembers;
using Domain.Entities.Teams;

namespace Application.Mappers
{
    public static class TeamsMapper
    {
        public static TeamDto MapToDto(this Team item)
        {
            return new TeamDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                SupervisorName = item.Supervisor.UserName!,
                SupervisorId = item.SupervisorId,
                MembersCount = item.Members.Count,
                Members = [.. item.Members.Select(e => e.MapToDto())],
            };
        }

        public static TeamMemberDto MapToDto(this TeamMember item)
        {
            return new TeamMemberDto
            {
                Id = item.Id,
                Username = item.User.UserName!,
                UserId = item.UserId,
                Role = item.Role,
                JoinedAt = item.JoinedAt,
            };
        }

        public static TeamMember MapToEntity(this AddMemberDto dto)
        {
            return new TeamMember
            {
                Role = dto.Role,
                UserId = dto.UserId,
                TeamId = dto.TeamId,
            };
        }
    }
}
