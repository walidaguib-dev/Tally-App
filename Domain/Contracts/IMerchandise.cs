using Domain.Entities;
using Domain.Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface IMerchandise
    {
        public Task<PagedResult<Merchandise>?> GetMerchandisesAsync(PaginationParams paginationParams, string? name, string? type);
        public Task<Merchandise?> GetOneAsync(int id);
        public Task<Merchandise> CreateOne(Merchandise merchandise);
        public Task<bool> UpdateOne(int id, string Name, string Type);
        public Task<bool> DeleteOne(int id);
    }
}
