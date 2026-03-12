using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface ITallySheetTruck
    {
        public Task<TallySheetTruck> AssignTruckAsync(TallySheetTruck sheetTruck);
        public Task<List<TallySheetTruck>> GetTallySheetTrucksAsync(int TallySheetId);
        public Task<bool?> EndTruckSessionTime(int TallySheetId, int TruckId, TimeOnly EndTime);
        public Task<bool?> DeleteAssignedTruck(int TallySheetId, int TruckId);
    }
}