using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Infrastructure.Repositories
{
    public class TallySheetMerchandiseRepository(
        ApplicationDbContext _context,
        ICaching _cachingService,
        IConnectionMultiplexer _connection
    ) : ITallySheetMerchandise
    {
        private readonly ApplicationDbContext context = _context;
        private readonly ICaching cachingService = _cachingService;
        private readonly IConnectionMultiplexer connection = _connection;
        public async Task<TallySheetMerchandise> AddMerchandise(TallySheetMerchandise item)
        {
            await context.TallySheetMerchandises.AddAsync(item);
            await context.SaveChangesAsync();
            return item;
        }

        public async Task<bool?> DeleteMerchandise(int tallySheetId, int MerchandiseId)
        {
            var result = await context.TallySheetMerchandises
                        .Where(x => x.TallySheetId == tallySheetId && x.MerchandiseId == MerchandiseId)
                        .ExecuteDeleteAsync();
            return result == 0 ? null : true;
        }

        public async Task<TallySheetMerchandise?> GetById(int tallySheetId, int MerchandiseId)
        {
            var key = $"Operation_{tallySheetId}_{MerchandiseId}";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    return await context.TallySheetMerchandises
                        .AsNoTracking()
                        .Include(p => p.Merchandise)
                        .FirstOrDefaultAsync(x => x.MerchandiseId == MerchandiseId && x.TallySheetId == tallySheetId, token);
                },
                TimeSpan.FromMinutes(10)
            );

            return result;
        }

        public async Task<List<TallySheetMerchandise>> GetByTallySheet(int tallySheetId)
        {
            var key = $"Operations_{tallySheetId}";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    return await context.TallySheetMerchandises
                    .AsNoTracking()
                    .Include(p => p.Merchandise)
                                .Where(x => x.TallySheetId == tallySheetId)
                                .ToListAsync(token);
                },
                TimeSpan.FromMinutes(10)
            );

            return result ?? [];
        }


        public async Task<bool?> QueueQuantityUpdate(int tallySheetId, int MerchandiseId, int quantity)
        {
            var exists = await context.TallySheetMerchandises
                .AsNoTracking()
                .AnyAsync(x => x.TallySheetId == tallySheetId && x.MerchandiseId == MerchandiseId);
            if (!exists) return false;
            var db = connection.GetDatabase();
            var key = $"quantity_pending_{tallySheetId}_{MerchandiseId}";
            await db.StringSetAsync(key, quantity, TimeSpan.FromMinutes(30));
            return true;
        }
    }
}