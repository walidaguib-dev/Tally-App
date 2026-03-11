using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TallySheetTrucksRepository(
        ApplicationDbContext _context
    ) : ITallySheetTruck
    {
        private readonly ApplicationDbContext context = _context;
        public async Task<TallySheetTruck> AssignTruckAsync(TallySheetTruck sheetTruck)
        {
            await context.TallySheetTrucks.AddAsync(sheetTruck);
            await context.SaveChangesAsync();
            return sheetTruck;
        }

        public async Task<bool?> EndTruckSessionTime(int id, TimeOnly EndTime)
        {
            var affectedRow = await context.TallySheetTrucks
                        .Where(x => x.Id == id)
                        .ExecuteUpdateAsync(s => s.SetProperty(p => p.EndTime, EndTime));
            return affectedRow == 0 ? null : true;
        }

        public async Task<List<TallySheetTruck>> GetTallySheetTrucksAsync(int tallySessionId)
        {
            var result = await context.TallySheetTrucks
                    .Include(r => r.TallySheet)
                    .Include(r => r.Truck)
                  .Where(x => x.TallySheetId == tallySessionId)
                  .ToListAsync();
            return result;
        }
    }
}