using Domain.Contracts;
using Domain.Entities;
using Domain.Helpers.Pagination;
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
            await _context.Ships.AddAsync(ship);
            await _context.SaveChangesAsync();
            await _cachingService.RemoveByTagAsync("ships");
            return ship;
        }

        public async Task<Ship?> DeleteShip(int id)
        {
            var ship = await _context.Ships.FindAsync(id);
            if (ship == null) return null;
            _context.Ships.Remove(ship);
            await _context.SaveChangesAsync();
            await _cachingService.RemoveByTagAsync("ships");
            await _cachingService.RemoveCaching($"ship_{id}");
            return ship;
        }

        public async Task<PagedResult<Ship>?> GetAllShips(PaginationParams paginationParams, string? name)
        {
            var key = $"ships_page{paginationParams.PageNumber}_size{paginationParams.PageSize}_sort{paginationParams.SortBy ?? "none"}_desc{paginationParams.IsDescending}_name{name ?? "all"}";
            var result = await _cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    var query = _context.Ships.AsQueryable();
                    if (!string.IsNullOrEmpty(name))
                        query = query.Where(c => c.Name.Contains(name));

                    var totalCount = await query.CountAsync(cancellationToken: token);

                    // Sorting
                    query = paginationParams.SortBy?.ToLower() switch
                    {
                        "name" => paginationParams.IsDescending
                            ? query.OrderByDescending(c => c.Name)
                            : query.OrderBy(c => c.Name),
                        _ => query.OrderBy(c => c.Id)
                    };

                    // Pagination
                    var items = await query
                        .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                        .Take(paginationParams.PageSize)
                        .ToListAsync(token);

                    return new PagedResult<Ship>
                    {
                        Items = items,
                        TotalCount = totalCount, // ← filtered count
                        PageNumber = paginationParams.PageNumber,
                        PageSize = paginationParams.PageSize
                    };
                }
                ,
                TimeSpan.FromMinutes(10)
                , tags: ["ships"]);
            return result;
        }

        public async Task<Ship?> GetShipById(int id)
        {
            var key = $"ship_{id}";

            var cachedShip = await _cachingService.GetOrSetAsync(key,
                async token => await _context.Ships.FirstOrDefaultAsync(s => s.Id == id, cancellationToken: token),
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
            await _cachingService.RemoveByTagAsync("ships");
            await _cachingService.RemoveCaching($"ship_{id}");
            return ship;
        }
    }
}
