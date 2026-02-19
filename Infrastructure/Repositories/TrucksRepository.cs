using Domain.Contracts;
using Domain.Entities;
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

        public async Task<List<Truck>> GetAll()
        {
            var key = "trucks";
            var cachedTrucks = await cachingService.GetOrSetAsync(
                key,
                async token => await context.Trucks.ToListAsync(cancellationToken: token),
                TimeSpan.FromHours(1)
                );
            return cachedTrucks is null ? [] : cachedTrucks;
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
