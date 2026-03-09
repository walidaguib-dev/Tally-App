using Domain.Entities;
using Domain.Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface ITrucks
    {
        public Task<PagedResult<Truck>?> GetAll(PaginationParams paginationParams, string? plateNumber);
        public Task<Truck?> GetOne(int Id);
        public Task<Truck> CreateOne(Truck truck);
        public Task<bool?> UpdateOne(int Id, string PlateNumber, double Capacity);
        public Task<bool?> DeleteOne(int Id);
    }
}
