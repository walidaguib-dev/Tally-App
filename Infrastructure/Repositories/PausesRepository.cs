using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PausesRepository(ApplicationDbContext _context, ICaching cachingService) : IPauses
    {
        private readonly ApplicationDbContext context = _context;
        private readonly ICaching _cachingService = cachingService;

        public async Task<Pause> CreatePause(Pause pause)
        {
            await context.Pauses.AddAsync(pause);
            await context.SaveChangesAsync();
            return pause;
        }

        public async Task<bool?> DeletePause(int id)
        {
            var affectedRow = await context.Pauses.Where(x => x.Id == id).ExecuteDeleteAsync();
            return affectedRow == 0 ? null : true;
        }

        public async Task<bool?> EndPause(int id, TimeOnly endTime)
        {
            var affectedRow = await context
                .Pauses.Where(x => x.Id == id)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.EndTime, endTime));
            return affectedRow == 0 ? null : true;
        }

        public async Task<Pause?> GetById(int Id)
        {
            var key = $"pause_{Id}";
            var result = await _cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    return await context
                        .Pauses.AsNoTracking()
                        .Include(x => x.Truck)
                        .Include(x => x.TallySheet)
                        .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken: token);
                },
                TimeSpan.FromMinutes(15),
                ["pause"]
            );
            return result;
        }

        public async Task<List<Pause>> GetPausesByTallySession(int tallySessionId)
        {
            var key = $"pauses_{tallySessionId}";
            var result = await _cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    return await context
                        .Pauses.AsNoTracking()
                        .Include(x => x.Truck)
                        .Include(x => x.TallySheet)
                        .Where(x => x.TallySheetId == tallySessionId)
                        .ToListAsync();
                },
                TimeSpan.FromMinutes(10),
                ["pauses"]
            );

            return result ?? [];
        }

        public async Task<bool?> UpdatePause(int id, UpdatePauseObject updatePauseObject)
        {
            var affectedRow = await context
                .Pauses.Where(x => x.Id == id)
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(
                            p => p.Reason,
                            Enum.TryParse<PauseReason>(
                                updatePauseObject.Reason,
                                true,
                                out var reason
                            )
                                ? reason
                                : PauseReason.Other
                        )
                        .SetProperty(p => p.StartTime, updatePauseObject.StartTime)
                        .SetProperty(p => p.Notes, updatePauseObject.Notes)
                        .SetProperty(p => p.TruckId, updatePauseObject.TruckId)
                );
            return affectedRow == 0 ? null : true;
        }
    }
}
