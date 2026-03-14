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

        public Task<TallySheetMerchandise?> GetById(int tallySheetId, int MerchandiseId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TallySheetMerchandise>> GetByTallySheet(int tallySheetId)
        {
            throw new NotImplementedException();
        }


        public Task QueueQuantityUpdate(int tallySheetId, int MerchandiseId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}