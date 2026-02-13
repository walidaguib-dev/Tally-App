using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ZiggyCreatures.Caching.Fusion;

namespace Infrastructure.Repositories
{
    public class ShipsRepository(
        ApplicationDbContext context,
        ICaching cachingService
        ) : IShips
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ICaching _cachingService = cachingService;
        public async Task<Ship> CreateShip(Ship ship)
        {
             await  _context.Ships.AddAsync(ship);
             await _context.SaveChangesAsync();
            await _cachingService.RemoveCaching("ships");
            return ship;
        }

        public async Task<Ship?> DeleteShip(int id)
        {
            var ship = await _context.Ships.FindAsync(id);
            if (ship == null) return null;
            _context.Ships.Remove(ship);
            await _context.SaveChangesAsync();
            await _cachingService.RemoveCaching("ships");
            await _cachingService.RemoveCaching($"ship_{id}");
            return ship;
        }

        public async Task<List<Ship>> GetAllShips()
        {
            var key = "ships";
            var cachedShips = await _cachingService.GetOrSetAsync(
                key,
                async token => await _context.Ships.ToListAsync()
                , 
                TimeSpan.FromMinutes(10));

            if (cachedShips is null) return [];

            return cachedShips;
        }

        public async Task<Ship?> GetShipById(int id)
        {
            var key = $"ship_{id}";
            
            var cachedShip = await _cachingService.GetOrSetAsync(key, 
                async token => await _context.Ships.FirstOrDefaultAsync(s => s.Id == id,cancellationToken:token), 
                TimeSpan.FromMinutes(10));
            if (cachedShip is null) return null;
            return cachedShip;
        }

        public async Task<Ship?> UpdateShip(int id, string Name, string IMO)
        {
            var ship = await _context.Ships.FindAsync(id);
            if (ship == null) return null;
            ship.Name = Name;
            ship.ImoNumber = IMO;
            await _context.SaveChangesAsync();
            await _cachingService.RemoveCaching("ships");
            await _cachingService.RemoveCaching($"ship_{id}");
            return ship;
        }
    }
}
