using Domain.Contracts;
using Domain.Entities;
using Domain.Helpers.Pagination;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZiggyCreatures.Caching.Fusion;

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
                async token => await context.Clients
                .AsNoTracking()
                .Include(c => c.Merchandise)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken: token),
                TimeSpan.FromHours(1));
            return result is null ? null : result;
        }

        public async Task<PagedResult<Client>?> GetAll(PaginationParams paginationParams, string? name)
        {
            var key = $"clients_page{paginationParams.PageNumber}_size{paginationParams.PageSize}_sort{paginationParams.SortBy ?? "none"}_desc{paginationParams.IsDescending}_name{name ?? "all"}";

            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    var query = context.Clients
                        .AsNoTracking()
                        .Include(c => c.Merchandise)
                        .AsQueryable();

                    // Filtering
                    if (!string.IsNullOrEmpty(name))
                        query = query.Where(c => c.Name.Contains(name));

                    var totalCount = await query.CountAsync(cancellationToken: token);

                    // Sorting
                    query = paginationParams.SortBy?.ToLower() switch
                    {
                        "name" => paginationParams.IsDescending
                            ? query.OrderByDescending(c => c.Name)
                            : query.OrderBy(c => c.Name),
                        _ => query.OrderBy(c => c.Id)
                    };

                    // Pagination
                    var items = await query
                        .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                        .Take(paginationParams.PageSize)
                        .ToListAsync(token);

                    return new PagedResult<Client>
                    {
                        Items = items,
                        TotalCount = totalCount, // ← filtered count
                        PageNumber = paginationParams.PageNumber,
                        PageSize = paginationParams.PageSize
                    };
                },
                TimeSpan.FromMinutes(10),
                tags: ["clients"]
            );
            return result;

        }

        public async Task<object?> UpdateOne(int id, string name, string contact, List<string> Bill_of_lading, int merchandiseId)
        {
            var affectedRows = await context.Clients
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(
                  x => x.SetProperty(p => p.Name, name)
                       .SetProperty(p => p.ContactInfo, contact)
                       .SetProperty(p => p.Bill_Of_Lading, Bill_of_lading)
                       .SetProperty(p => p.MerchandiseId, merchandiseId)
                );

            if (affectedRows == 0) return null;
            return "client updated";
        }
    }
}
