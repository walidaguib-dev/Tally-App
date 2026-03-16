using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Infrastructure.Repositories
{
    public class QuantitySyncRepository(
        ApplicationDbContext _context,
        IConnectionMultiplexer _connection
    ) : IQuantitySync
    {
        private readonly ApplicationDbContext context = _context;
        private readonly IConnectionMultiplexer connection = _connection;

        public async Task SyncPendingQuantities()
        {
            var db = connection.GetDatabase();
            var server = connection.GetServer(connection.GetEndPoints().First());
            var keys = server.Keys(pattern: "quantity_pending_*").ToArray();

            foreach (var key in keys)
            {
                var value = await db.StringGetAsync(key);
                if (!value.HasValue)
                    continue;

                var parts = key.ToString().Replace("quantity_pending_", "").Split('_');
                var tallySheetId = int.Parse(parts[0]);
                var clientId = int.Parse(parts[1]);
                var quantity = int.Parse(value!);

                var rows = await context
                    .TallySheetClients.Where(x =>
                        x.TallySheetId == tallySheetId && x.ClientId == clientId
                    )
                    .ExecuteUpdateAsync(x =>
                        x.SetProperty(p => p.Quantity, quantity)
                            .SetProperty(p => p.LastUpdated, DateTime.UtcNow)
                    );

                await db.KeyDeleteAsync(key);
            }
        }
    }
}
