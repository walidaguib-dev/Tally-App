using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Contracts
{
    public interface ITallySheet
    {
        public Task<List<TallySheet>> GetAllAsync();
        public Task<List<TallySheet>> GetTallySheetsByShip(int shipId);
        public Task<TallySheet> CreateAsync(TallySheet tallySheet);
        public Task<TallySheet?> GetOneAsync(int id);
        public Task<bool?> UpdateOneAsync(int id, DateTime Date, int TeamsCount, ShiftType Shift, ZoneType Zone, int ShipId);
        public Task<bool?> DeleteAsync(int id);
    }
}