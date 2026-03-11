using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IPauses
    {
        public Task<Pause> CreatePause(Pause pause);
        public Task<bool?> EndPause(int id, TimeOnly endTime);
        public Task<List<Pause>> GetPausesByTallySession(int tallySessionId);
        public Task<Pause?> GetById(int Id);
        public Task<bool?> DeletePause(int id);
    }
}