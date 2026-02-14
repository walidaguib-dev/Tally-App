using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface IClients
    {
        public Task<List<Client>> GetAll();
        public Task<Client?> Get(int id);
        public Task<Client> CreateOne(Client client);
        public Task<object?> UpdateOne(int id , string name , string contact , List<string> Bill_of_lading , int merchandiseId);
        public Task<object?> DeleteOne(int id);
    }
}
