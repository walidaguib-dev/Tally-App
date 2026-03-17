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
    public class ObservationsRepository(ApplicationDbContext _context, ICaching _cachingService)
        : IObservations
    {
        private readonly ApplicationDbContext context = _context;
        private readonly ICaching cachingService = _cachingService;

        public async Task<Observation> CreateOne(Observation observation)
        {
            await context.Observations.AddAsync(observation);
            await context.SaveChangesAsync();
            return observation;
        }

        public async Task<bool?> DeleteOne(int Id)
        {
            var affectedRow = await context
                .Observations.Where(x => x.Id == Id)
                .ExecuteDeleteAsync();
            return affectedRow == 0 ? null : true;
        }

        public async Task<List<Observation>> GetAllByTallyId(int tallySheetId)
        {
            var key = $"observations_{tallySheetId}";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    return await context
                        .Observations.Include(x => x.Client)
                        .AsNoTracking()
                        .Include(x => x.Truck)
                        .Where(x => x.TallySheetId == tallySheetId)
                        .ToListAsync(token);
                },
                TimeSpan.FromMinutes(10),
                ["observations"]
            );
            return result ?? [];
        }

        public async Task<Observation?> GetById(int Id)
        {
            var key = $"observation_{Id}";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    return await context
                        .Observations.AsNoTracking()
                        .Include(x => x.Client)
                        .Include(x => x.Truck)
                        .FirstOrDefaultAsync(x => x.Id == Id, token);
                },
                TimeSpan.FromMinutes(10),
                ["observation"]
            );

            return result;
        }

        public async Task<bool?> UpdateOne(int Id, UpdateObservationRequest request)
        {
            var affectedRow = await context
                .Observations.Where(x => x.Id == Id)
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(p => p.Description, request.Description)
                        .SetProperty(
                            p => p.Type,
                            Enum.TryParse<ObservationType>(request.Type, true, out var type)
                                ? type
                                : ObservationType.Other
                        )
                        .SetProperty(p => p.TallySheetId, request.TallySheetId)
                        .SetProperty(p => p.ClientId, request.ClientId)
                        .SetProperty(p => p.TruckId, request.TruckId)
                        .SetProperty(p => p.Timestamp, request.Timestamp)
                );
            return affectedRow == 0 ? null : true;
        }
    }
}
