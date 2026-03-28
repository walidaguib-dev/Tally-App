using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Requests;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CarsRepository(ApplicationDbContext _context, ICaching _cachingService) : ICars
    {
        private readonly ApplicationDbContext context = _context;
        private readonly ICaching cachingService = _cachingService;

        public async Task<Cars> CreateOne(Cars cars)
        {
            var result = await context.Cars.AddAsync(cars);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool?> DeleteOne(int Id)
        {
            var affectedRow = await context.Cars.Where(x => x.Id == Id).ExecuteDeleteAsync();
            return affectedRow == 0 ? null : true;
        }

        public async Task<List<Cars>> GetAllCarsByTallySession(int TallySessionId)
        {
            var key = $"cars_{TallySessionId}";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    return await context
                        .Cars.Where(x => x.TallySheetId == TallySessionId)
                        .Include(c => c.Ship)
                        .Include(c => c.User)
                        .Include(c => c.TallySheet)
                        .ToListAsync(token);
                },
                TimeSpan.FromMinutes(10),
                ["cars"]
            );
            return result ?? [];
        }

        public async Task<Cars?> GetCarAsync(int Id)
        {
            var key = $"car_{Id}";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    return await context.Cars
                    .Include(x => x.Ship)
                    .Include(x => x.User)
                    .Include(x => x.TallySheet)
                    .FirstOrDefaultAsync(x => x.Id == Id, token);
                },
                TimeSpan.FromMinutes(10),
                ["car"]
            );

            return result;
        }

        public async Task<bool?> UpdateOne(int Id, UpdateCarsRequest request)
        {
            var carStatus = Enum.TryParse<CarStatus>(request.carStatus, true, out var status)
                ? status
                : CarStatus.Pending;
            var affectedRow = await context
                .Cars.Where(x => x.Id == Id)
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(p => p.Brand, request.Brand)
                        .SetProperty(p => p.Type, request.Type)
                        .SetProperty(p => p.VinNumber, request.VinNumber)
                        .SetProperty(p => p.Bill_Of_Lading, request.Bill_Of_Lading)
                        .SetProperty(p => p.ShipId, request.ShipId)
                );

            return affectedRow == 0 ? null : true;
        }
    }
}
