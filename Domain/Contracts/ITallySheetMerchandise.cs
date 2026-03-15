using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface ITallySheetMerchandise
    {
        Task<TallySheetMerchandise> AddMerchandise(TallySheetMerchandise item);
        Task<TallySheetMerchandise?> GetById(int tallySheetId, int MerchandiseId);
        Task<List<TallySheetMerchandise>> GetByTallySheet(int tallySheetId);
        Task<bool?> DeleteMerchandise(int tallySheetId, int MerchandiseId);

        // Write-behind quantity update — Redis only, no DB
        public Task<bool?> QueueQuantityUpdate(int tallySheetId, int MerchandiseId, int quantity);
    }
}