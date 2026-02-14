using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class ClientsRepository(
        ApplicationDbContext _context,
        ICaching _cachingService
        ) : IClients
    {
        private readonly ApplicationDbContext context = _context;
        private readonly ICaching cachingService = _cachingService;
        public async Task<Client> CreateOne(Client client)
        {
            var Merchandise = await context.Merchandises.Where(x => x.Id == client.MerchandiseId).FirstAsync() ?? throw new Exception("merchandise invalid");
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
            return client;
        }

        public async Task<object?> DeleteOne(int id)
        {
            var result = await context.Clients
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();
            if (result == 0) return null;
            return "client deleted!";
        }

        public async Task<Client?> Get(int id)
        {
            var key = $"client_{id}";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token => await context.Clients.FirstOrDefaultAsync(c => c.Id == id, cancellationToken: token),
                TimeSpan.FromHours(1));
            return result is null ? null : result;
        }

        public async Task<List<Client>> GetAll()
        {
            var key = "clients";
            var result = await cachingService.GetOrSetAsync
                (
                  key,
                  async token => await context.Clients.Include(c => c.Merchandise).ToListAsync(cancellationToken: token),
                  TimeSpan.FromHours(1)
                );
            return result is null ? [] : result ;
        }

        public async Task<object?> UpdateOne(int id, string name, string contact, List<string> Bill_of_lading, int merchandiseId)
        {
            var affectedRows = await context.Clients
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(
                  x => x.SetProperty( p => p.Name , name)
                       .SetProperty(p => p.ContactInfo , contact)
                       .SetProperty(p => p.Bill_Of_Lading , Bill_of_lading)
                       .SetProperty(p => p.MerchandiseId , merchandiseId)
                );

            if (affectedRows == 0) return null;
            return "client updated";
        }
    }
}
