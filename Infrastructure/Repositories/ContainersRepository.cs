using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Helpers.Pagination;
using Domain.Requests;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ContainersRepository(ApplicationDbContext _context, ICaching _cachingService)
        : IContainers
    {
        private readonly ApplicationDbContext context = _context;
        private readonly ICaching cachingService = _cachingService;

        public async Task<Container> CreateAsync(Container container)
        {
            await context.Containers.AddAsync(container);
            await context.SaveChangesAsync();
            return container;
        }

        public async Task<bool?> DeleteAsync(string ContainerNumber)
        {
            var affectedRow = await context
                .Containers.Where(x => x.ContainerNumber == ContainerNumber)
                .ExecuteDeleteAsync();
            return affectedRow == 0 ? null : true;
        }

        public async Task<PagedResult<Container>?> GetAll(
            PaginationParams paginationParams,
            string? ContainerNumber,
            string? Bill_Of_Lading,
            string? ClientName
        )
        {
            var key =
                $"containers_page{paginationParams.PageNumber}_size{paginationParams.PageSize}_sort{paginationParams.SortBy ?? "none"}_desc{paginationParams.IsDescending}_ClientName{ClientName ?? "all"}_ContainerNumber{ContainerNumber ?? "all"}_bill_of_lading{Bill_Of_Lading ?? "all"}";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    var query = context
                        .Containers.Include(x => x.TallySheet)
                        .Include(x => x.Client)
                        .Include(x => x.user)
                        .AsQueryable();

                    if (!string.IsNullOrEmpty(ContainerNumber))
                        query = query.Where(c => c.ContainerNumber.Contains(ContainerNumber));
                    if (!string.IsNullOrEmpty(Bill_Of_Lading))
                        query = query.Where(c => c.Bill_of_lading!.Contains(Bill_Of_Lading));
                    if (!string.IsNullOrEmpty(ClientName))
                        query = query.Where(c => c.Client.Name.Contains(ClientName));

                    var totalCount = await query.CountAsync(cancellationToken: token);

                    query = paginationParams.SortBy?.ToLower() switch
                    {
                        "Bill_Of_Lading" => paginationParams.IsDescending
                            ? query.OrderByDescending(c => c.Bill_of_lading)
                            : query.OrderBy(c => c.Bill_of_lading),
                        "ClientName" => paginationParams.IsDescending
                            ? query.OrderByDescending(c => c.Client.Name)
                            : query.OrderBy(c => c.Client.Name),
                        _ => query.OrderBy(c => c.Id),
                    };

                    var items = await query
                        .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                        .Take(paginationParams.PageSize)
                        .ToListAsync(token);

                    return new PagedResult<Container>
                    {
                        Items = items,
                        TotalCount = totalCount, // ← filtered count
                        PageNumber = paginationParams.PageNumber,
                        PageSize = paginationParams.PageSize,
                    };
                },
                TimeSpan.FromMinutes(10),
                ["containers"]
            );
            return result;
        }

        public async Task<List<Container>> GetAllByTallySession(int TallySheetSessionId)
        {
            var key = $"containers_{TallySheetSessionId}";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    return await context
                        .Containers.Include(x => x.TallySheet)
                        .Include(x => x.Client)
                        .Include(x => x.user)
                        .Where(x => x.TallySheetId == TallySheetSessionId)
                        .ToListAsync(token);
                },
                TimeSpan.FromMinutes(10),
                ["containers"]
            );
            return result ?? [];
        }

        public async Task<Container?> GetAsync(string ContainerNumber)
        {
            var key = $"container_{ContainerNumber}";
            var result = await cachingService.GetOrSetAsync(
                key,
                async token =>
                {
                    return await context
                        .Containers.Include(x => x.TallySheet)
                        .Include(x => x.Client)
                        .Include(x => x.user)
                        .FirstOrDefaultAsync(x => x.ContainerNumber == ContainerNumber);
                },
                TimeSpan.FromMinutes(10),
                ["container"]
            );
            return result;
        }

        public async Task<bool?> UpdateAsync(string ContainerNumber, UpdateContainerRequest request)
        {
            var containerSize = Enum.TryParse<ContainerSize>(
                request.ContainerSize,
                true,
                out var size
            )
                ? size
                : ContainerSize.Forty;
            var containerType = Enum.TryParse<ContainerType>(
                request.ContainerType,
                true,
                out var type
            )
                ? type
                : ContainerType.OpenTop;
            var containerStatus = Enum.TryParse<ContainerStatus>(
                request.ContainerStatus,
                true,
                out var status
            )
                ? status
                : ContainerStatus.Pending;
            var affectedRow = await context
                .Containers.Where(x => x.ContainerNumber == ContainerNumber)
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(p => p.ContainerNumber, request.ContainerNumber)
                        .SetProperty(p => p.Bill_of_lading, request.Bill_of_lading)
                        .SetProperty(p => p.ContainerSize, containerSize)
                        .SetProperty(p => p.ContainerStatus, containerStatus)
                        .SetProperty(p => p.ContainerType, containerType)
                        .SetProperty(p => p.SealNumber, request.SealNumber)
                        .SetProperty(p => p.ClientId, request.ClientId)
                );
            return affectedRow == 0 ? null : true;
        }
    }
}
