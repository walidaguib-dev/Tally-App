using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PausesRepository(
        ApplicationDbContext _context
    ) : IPauses
    {
        private readonly ApplicationDbContext context = _context;
        public async Task<Pause> CreatePause(Pause pause)
        {
            await context.Pauses.AddAsync(pause);
            await context.SaveChangesAsync();
            return pause;
        }

        public async Task<bool?> DeletePause(int id)
        {
            var affectedRow = await context.Pauses
                        .Where(x => x.Id == id)
                        .ExecuteDeleteAsync();
            return affectedRow == 0 ? null : true;
        }

        public async Task<bool?> EndPause(int id, TimeOnly endTime)
        {
            var affectedRow = await context.Pauses
                    .Where(x => x.Id == id)
                    .ExecuteUpdateAsync(s => s.SetProperty(p => p.EndTime, endTime));
            return affectedRow == 0 ? null : true;
        }

        public async Task<Pause?> GetById(int Id)
        {
            return await context.Pauses.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public Task<List<Pause>> GetPausesByTallySession(int tallySessionId)
        {
            return context.Pauses.Where(x => x.TallySheetId == tallySessionId).ToListAsync();
        }
    }
}