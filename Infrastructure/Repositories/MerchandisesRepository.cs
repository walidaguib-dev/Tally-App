using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZiggyCreatures.Caching.Fusion;

namespace Infrastructure.Repositories
{
    public class MerchandisesRepository(
        ApplicationDbContext context,
        ICaching _cache

        ) : IMerchandise
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ICaching cache = _cache;

        public async Task<Merchandise> CreateOne(Merchandise merchandise)
        {
            await _context.Merchandises.AddAsync(merchandise);
            await _context.SaveChangesAsync();
            return merchandise;
        }

        public async Task<bool> DeleteOne(int id)
        {
 
            var affectedRows = await _context.Merchandises
                    .Where(m => m.Id == id)
                    .ExecuteDeleteAsync();

            if (affectedRows == 0) return false;
            return true;

        }

        public async Task<List<Merchandise>> GetMerchandisesAsync()
        {
            var key = "merchandises";
            var merchandiseList = await cache.GetOrSetAsync(key,
                async token => await _context.Merchandises.ToListAsync(cancellationToken:token),
                TimeSpan.FromHours(1)
                );

            return merchandiseList ?? [];

           
        }

        public async Task<Merchandise?> GetOneAsync(int id)
        {
            var key = $"merchandise_{id}";
            var cachedMerchandise = await cache.GetOrSetAsync(key,
                async token => await _context.Merchandises.FirstOrDefaultAsync(m => m.Id == id, cancellationToken: token),
                TimeSpan.FromHours(1));

            return cachedMerchandise ?? null;
        }

        public async Task<bool> UpdateOne(int id, string Name, string Type)
        {

            var affectedRows =  await _context.Merchandises
                    .Where(m => m.Id == id)
                    .ExecuteUpdateAsync(s => s.SetProperty(n => n.Name , Name).SetProperty( n => n.Type , Type));

            if (affectedRows == 0) return false;
            return true;
            
        }
    }
}
