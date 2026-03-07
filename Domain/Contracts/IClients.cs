using Domain.Entities;
using Domain.Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface IClients
    {
        public Task<PagedResult<Client>?> GetAll(PaginationParams paginationParams, string? name);
        public Task<Client?> Get(int id);
        public Task<Client> CreateOne(Client client);
        public Task<object?> UpdateOne(int id, string name, string contact, List<string> Bill_of_lading, int merchandiseId);
        public Task<object?> DeleteOne(int id);
    }
}
