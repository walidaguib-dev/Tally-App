using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Requests;

namespace Domain.Contracts
{
    public interface IObservations
    {
        public Task<List<Observation>> GetAllByTallyId(int tallySheetId);
        public Task<Observation?> GetById(int Id);
        public Task<Observation> CreateOne(Observation observation);
        public Task<bool?> UpdateOne(int Id, UpdateObservationRequest request);
        public Task<bool?> DeleteOne(int Id);
    }
}
