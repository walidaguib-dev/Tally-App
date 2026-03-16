using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface ITallySheetClient
    {
        Task<TallySheetClient> AddMerchandise(TallySheetClient item);
        Task<TallySheetClient?> GetById(int tallySheetId, int ClientId);
        Task<List<TallySheetClient>> GetByTallySheet(int tallySheetId);
        Task<bool?> DeleteMerchandise(int tallySheetId, int ClientId);

        // Write-behind quantity update — Redis only, no DB
        public Task<bool?> QueueQuantityUpdate(int tallySheetId, int ClientId, int quantity);
    }
}