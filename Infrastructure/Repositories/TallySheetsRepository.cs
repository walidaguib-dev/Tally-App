using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TallySheetsRepository(
        ApplicationDbContext _context
    ) : ITallySheet
    {
        private readonly ApplicationDbContext context = _context;
        public async Task<TallySheet> CreateAsync(TallySheet tallySheet)
        {
            await context.TallySheets.AddAsync(tallySheet);
            await context.SaveChangesAsync();
            return tallySheet;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            var affectedRow = await context.TallySheets
                        .Where(x => x.Id == id)
                        .ExecuteDeleteAsync();
            if (affectedRow == 0) return null;
            return true;
        }

        public async Task<List<TallySheet>> GetAllAsync()
        {
            var tallySheets = await context.TallySheets.ToListAsync();
            return tallySheets;
        }

        public async Task<TallySheet?> GetOneAsync(int id)
        {
            var tallySheet = await context.TallySheets.FirstOrDefaultAsync(x => x.Id == id) ?? null;
            return tallySheet;
        }

        public async Task<List<TallySheet>> GetTallySheetsByShip(int shipId)
        {
            var tallySheets = await context.TallySheets
                        .Where(x => x.ShipId == shipId)
                        .ToListAsync();
            return tallySheets;
        }

        public async Task<bool?> UpdateOneAsync(int id, DateTime Date, int TeamsCount, ShiftType Shift, ZoneType Zone, int ShipId)
        {
            var tallySheet = await context.TallySheets
                        .Where(x => x.Id == id)
                        .ExecuteUpdateAsync(
                            x => x.SetProperty(p => p.Date, Date)
                                .SetProperty(p => p.TeamsCount, TeamsCount)
                                .SetProperty(p => p.Shift, Shift)
                                .SetProperty(p => p.Zone, Zone)
                                .SetProperty(p => p.ShipId, ShipId));
            if (tallySheet == 0) return null;
            return true;
        }
    }
}