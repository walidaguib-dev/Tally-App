using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
            await _cachingService.RemoveByPattern("ships");
            await _cachingService.RemoveByPattern("ship");
            return ship;
        }

        public async Task<Ship?> DeleteShip(int id)
        {
            var ship = await _context.Ships.FindAsync(id);
            if (ship == null) return null;
            await _cachingService.RemoveByPattern("ships");
            await _cachingService.RemoveByPattern("ship");
            _context.Ships.Remove(ship);
            await _context.SaveChangesAsync();
            return ship;
        }

        public async Task<List<Ship>> GetAllShips()
        {
            var cachedShips = await _cachingService.GetFromCacheAsync<List<Ship>>("ships");
            if(cachedShips != null)
            {
                return cachedShips;
            }
            var ships = await _context.Ships.ToListAsync();
            await _cachingService.SetAsync("ships", ships, TimeSpan.FromMinutes(30));
            return ships;

        }

        public async Task<Ship?> GetShipById(int id)
        {
            var cachedShips = await _cachingService.GetFromCacheAsync<Ship>("ship");
            if (cachedShips != null)
            {
                return cachedShips;
            }
            var ship = await _context.Ships.FindAsync(id);
            if (ship == null) return null;
            await _cachingService.SetAsync("ship", ship, TimeSpan.FromMinutes(30));
            return ship;
        }

        public async Task<Ship?> UpdateShip(int id, string Name, string IMO)
        {
            var ship = await _context.Ships.FindAsync(id);
            if (ship == null) return null;
            ship.Name = Name;
            ship.ImoNumber = IMO;
            await _context.SaveChangesAsync();
            await _cachingService.RemoveByPattern("ships");
            await _cachingService.RemoveByPattern("ship");
            return ship;
        }
    }
}
