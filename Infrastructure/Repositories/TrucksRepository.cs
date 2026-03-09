using Domain.Contracts;
using Domain.Entities;
using Domain.Helpers.Pagination;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class TrucksRepository(
        ApplicationDbContext _context,
        ICaching _cachingService
        ) : ITrucks
    {
        private readonly ApplicationDbContext context = _context;
        private readonly ICaching cachingService = _cachingService;
        public async Task<Truck> CreateOne(Truck truck)
        {
            await context.Trucks.AddAsync(truck);
            await context.SaveChangesAsync();
            return truck;
        }

        public async Task<bool?> DeleteOne(int Id)
        {
            var affectedRow = await context.Trucks.
                Where(x => x.Id == Id)
                .ExecuteDeleteAsync();
            if (affectedRow == 0) return null;
            return true;

        }

        public async Task<PagedResult<Truck>?> GetAll(PaginationParams paginationParams, string? plateNumber)
        {
            var key = $"trucks_page{paginationParams.PageNumber}_size{paginationParams.PageSize}_sort{paginationParams.SortBy ?? "none"}_desc{paginationParams.IsDescending}_name{plateNumber ?? "all"}";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    var query = context.Trucks.AsQueryable();

                    if (!string.IsNullOrEmpty(plateNumber) || !string.IsNullOrWhiteSpace(plateNumber))
                        query = query.Where(q => q.PlateNumber == plateNumber);

                    var totalCount = await query.CountAsync(cancellationToken: token);

                    query = paginationParams.SortBy?.ToLower() switch
                    {
                        "plateNumber" => paginationParams.IsDescending
                            ? query.OrderByDescending(c => c.PlateNumber)
                            : query.OrderBy(c => c.PlateNumber),
                        _ => query.OrderBy(c => c.Id)
                    };

                    var items = await query
                        .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                        .Take(paginationParams.PageSize)
                        .ToListAsync(token);

                    return new PagedResult<Truck>
                    {
                        Items = items,
                        TotalCount = totalCount, // ← filtered count
                        PageNumber = paginationParams.PageNumber,
                        PageSize = paginationParams.PageSize
                    };
                },
                TimeSpan.FromHours(1),
                tags: ["trucks"]
                );
            return result;
        }

        public async Task<Truck?> GetOne(int Id)
        {
            var key = $"truck_{Id}";
            var cachedTruck = await cachingService.GetOrSetAsync(
                key,
                async token => await context.Trucks.Where(x => x.Id == Id).FirstAsync(),
                TimeSpan.FromHours(1)
                );
            return cachedTruck is null ? null : cachedTruck;
        }

        public async Task<bool?> UpdateOne(int Id, string PlateNumber, double Capacity)
        {
            var affectedRow = await context.Trucks
                .Where(x => x.Id == Id)
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.PlateNumber, PlateNumber).SetProperty(x => x.Capacity, Capacity));
            if (affectedRow == 0) return null;
            return true;
        }
    }
}
