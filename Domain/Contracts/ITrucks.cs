using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface ITrucks
    {
        public Task<List<Truck>> GetAll();
        public Task<Truck?> GetOne(int Id);
        public Task<Truck> CreateOne(Truck truck);
        public Task<bool?> UpdateOne(int Id, string PlateNumber, double Capacity);
        public Task<bool?> DeleteOne(int Id);
    }
}
