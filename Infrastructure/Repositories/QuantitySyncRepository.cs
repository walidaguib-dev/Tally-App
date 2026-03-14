using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;

namespace Infrastructure.Repositories
{
    public class QuantitySyncRepository : IQuantitySync
    {
        public Task SyncPendingQuantities()
        {
            throw new NotImplementedException();
        }
    }
}