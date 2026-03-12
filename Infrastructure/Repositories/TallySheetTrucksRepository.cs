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
        ApplicationDbContext _context,
        ICaching _cachingService
    ) : ITallySheetTruck
    {
        private readonly ApplicationDbContext context = _context;
        private readonly ICaching cachingService = _cachingService;
        public async Task<TallySheetTruck> AssignTruckAsync(TallySheetTruck sheetTruck)
        {
            await context.TallySheetTrucks.AddAsync(sheetTruck);
            await context.SaveChangesAsync();
            return sheetTruck;
        }

        public async Task<bool?> DeleteAssignedTruck(int TallySheetId, int TruckId)
        {
            var result = await context.TallySheetTrucks
                    .Where(x => x.TallySheetId == TallySheetId && x.TruckId == TruckId)
                    .ExecuteDeleteAsync();
            return result == 0 ? null : true;
        }

        public async Task<bool?> EndTruckSessionTime(int TallySheetId, int TruckId, TimeOnly EndTime)
        {
            var affectedRow = await context.TallySheetTrucks
                        .Where(x => x.TallySheetId == TallySheetId && x.TruckId == TruckId)
                        .ExecuteUpdateAsync(s => s.SetProperty(p => p.EndTime, EndTime));
            return affectedRow == 0 ? null : true;
        }

        public async Task<List<TallySheetTruck>> GetTallySheetTrucksAsync(int TallySheetId)
        {
            var key = $"TallySheetTrucks_{TallySheetId}";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    var result = await context.TallySheetTrucks
                    .Include(r => r.Truck)
                  .Where(x => x.TallySheetId == TallySheetId)
                  .ToListAsync(cancellationToken: token);
                    return result;
                },
                TimeSpan.FromMinutes(10)
            );

            return result ?? [];
        }
    }
}