using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Requests;

namespace Domain.Contracts
{
    public interface ICars
    {
        public Task<List<Cars>> GetAllCarsByTallySession(int TallySessionId);
        public Task<Cars?> GetCarAsync(int Id);
        public Task<Cars> CreateOne(Cars cars);
        public Task<bool?> UpdateOne(int Id, UpdateCarsRequest request);
        public Task<bool?> DeleteOne(int Id);
    }
}
