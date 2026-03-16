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
    public class TallySheetClientsRepository(
        ApplicationDbContext _context,
        ICaching _cachingService,
        IConnectionMultiplexer _connection
    ) : ITallySheetClient
    {
        private readonly ApplicationDbContext context = _context;
        private readonly ICaching cachingService = _cachingService;
        private readonly IConnectionMultiplexer connection = _connection;

        public async Task<TallySheetClient> AddMerchandise(TallySheetClient item)
        {
            await context.TallySheetClients.AddAsync(item);
            await context.SaveChangesAsync();
            return item;
        }

        public async Task<bool?> DeleteMerchandise(int tallySheetId, int ClientId)
        {
            var result = await context
                .TallySheetClients.Where(x =>
                    x.TallySheetId == tallySheetId && x.ClientId == ClientId
                )
                .ExecuteDeleteAsync();
            return result == 0 ? null : true;
        }

        public async Task<TallySheetClient?> GetById(int tallySheetId, int ClientId)
        {
            var key = $"Operation_{tallySheetId}_{ClientId}";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    return await context
                        .TallySheetClients.AsNoTracking()
                        .Include(p => p.Client)
                        .FirstOrDefaultAsync(
                            x => x.ClientId == ClientId && x.TallySheetId == tallySheetId,
                            token
                        );
                },
                TimeSpan.FromMinutes(10)
            );

            return result;
        }

        public async Task<List<TallySheetClient>> GetByTallySheet(int tallySheetId)
        {
            var key = $"Operations_{tallySheetId}";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    return await context
                        .TallySheetClients.AsNoTracking()
                        .Include(p => p.Client)
                        .Where(x => x.TallySheetId == tallySheetId)
                        .ToListAsync(token);
                },
                TimeSpan.FromMinutes(10)
            );

            return result ?? [];
        }

        public async Task<bool?> QueueQuantityUpdate(int tallySheetId, int clientId, int quantity)
        {
            var exists = await context
                .TallySheetClients.AsNoTracking()
                .AnyAsync(x => x.TallySheetId == tallySheetId && x.ClientId == clientId);
            if (!exists)
                return false;
            var db = connection.GetDatabase();
            var key = $"quantity_pending_{tallySheetId}_{clientId}";
            await db.StringSetAsync(key, quantity, TimeSpan.FromMinutes(30));
            return true;
        }
    }
}
