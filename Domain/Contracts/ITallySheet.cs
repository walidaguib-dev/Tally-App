using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.Helpers.Pagination;

namespace Domain.Contracts
{
    public interface ITallySheet
    {
        public Task<PagedResult<TallySheet>?> GetAllAsync(
            PaginationParams paginationParams, string? shipName, string? shift, string? zone, DateOnly? date);
        public Task<List<TallySheet>> GetTallySheetsByShip(int shipId);
        public Task<TallySheet> CreateAsync(TallySheet tallySheet);
        public Task<TallySheet?> GetOneAsync(int id);
        public Task<bool?> UpdateOneAsync(int id, int TeamsCount, ShiftType Shift, ZoneType Zone, int ShipId);
        public Task<bool?> DeleteAsync(int id);
    }
}