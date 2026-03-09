using Domain.Contracts;
using Domain.Entities;
using Domain.Helpers.Pagination;
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

        public async Task<PagedResult<Merchandise>?> GetMerchandisesAsync(PaginationParams paginationParams, string? name, string? type)
        {
            var key = $"merchandises_page{paginationParams.PageNumber}_size{paginationParams.PageSize}_sort{paginationParams.SortBy ?? "none"}_desc{paginationParams.IsDescending}_name{name ?? "all"}_type{type ?? "all"}";
            var result = await cache.GetOrSetAsync(key,
                async token =>
                {
                    var query = _context.Merchandises.AsQueryable();

                    if (!string.IsNullOrEmpty(name))
                        query = query.Where(c => c.Name.Contains(name));

                    if (!string.IsNullOrEmpty(type))
                        query = query.Where(c => c.Type.Contains(type));


                    var totalCount = await query.CountAsync(cancellationToken: token);

                    query = paginationParams.SortBy?.ToLower() switch
                    {
                        "name" => paginationParams.IsDescending
                            ? query.OrderByDescending(c => c.Name)
                            : query.OrderBy(c => c.Name),
                        _ => query.OrderBy(c => c.Id)
                    };

                    query = paginationParams.SortBy?.ToLower() switch
                    {
                        "type" => paginationParams.IsDescending
                            ? query.OrderByDescending(c => c.Type)
                            : query.OrderBy(c => c.Type),
                        _ => query.OrderBy(c => c.Id)
                    };
                    var items = await query
                        .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                        .Take(paginationParams.PageSize)
                        .ToListAsync(token);


                    return new PagedResult<Merchandise>
                    {
                        Items = items,
                        TotalCount = totalCount, // ← filtered count
                        PageNumber = paginationParams.PageNumber,
                        PageSize = paginationParams.PageSize
                    };


                },
                TimeSpan.FromHours(1),
                tags: ["merchandises"]
                );

            return result;


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

            var affectedRows = await _context.Merchandises
                    .Where(m => m.Id == id)
                    .ExecuteUpdateAsync(s => s.SetProperty(n => n.Name, Name).SetProperty(n => n.Type, Type));

            if (affectedRows == 0) return false;
            return true;

        }
    }
}
