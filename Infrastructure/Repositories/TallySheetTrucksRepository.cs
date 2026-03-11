using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Data;

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

        public Task<bool?> EndTruckSessionTime(int id, TimeOnly EndTime)
        {
            throw new NotImplementedException();
        }

        public Task<List<TallySheetTruck>> GetTallySheetTrucksAsync(int tallySessionId)
        {
            throw new NotImplementedException();
        }
    }
}