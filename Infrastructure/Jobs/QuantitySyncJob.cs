using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;

namespace Infrastructure.Jobs
{
    public class QuantitySyncJob(IQuantitySync _syncService)
    {
        private readonly IQuantitySync syncService = _syncService;

        public async Task SyncQuantities()
        {
            await syncService.SyncPendingQuantities();
        }
    }
}