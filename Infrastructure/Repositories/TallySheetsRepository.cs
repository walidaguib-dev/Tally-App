using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Helpers.Pagination;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TallySheetsRepository(
        ApplicationDbContext _context,
        ICaching cachingService
    ) : ITallySheet
    {
        private readonly ApplicationDbContext context = _context;
        private readonly ICaching caching = cachingService;
        public async Task<TallySheet> CreateAsync(TallySheet tallySheet)
        {
            await context.TallySheets.AddAsync(tallySheet);
            await context.SaveChangesAsync();
            return tallySheet;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            var affectedRow = await context.TallySheets
                        .Where(x => x.Id == id)
                        .ExecuteDeleteAsync();
            if (affectedRow == 0) return null;
            return true;
        }

        public async Task<PagedResult<TallySheet>?> GetAllAsync(
                 PaginationParams paginationParams,
                string? shipName,
                string? shift,
                string? zone,
                DateOnly? date)
        {
            var key = $"tallysheets_page{paginationParams.PageNumber}_size{paginationParams.PageSize}_sort{paginationParams.SortBy ?? "none"}_desc{paginationParams.IsDescending}_ship{shipName ?? "all"}_shift{shift ?? "all"}_zone{zone ?? "all"}_date{date?.ToString() ?? "all"}";

            return await caching.GetOrSetAsync(
                key,
                async token =>
                {
                    var query = context.TallySheets
                    .AsNoTracking()
                        .Include(x => x.Ship)
                        .AsQueryable();

                    // Filtering
                    if (!string.IsNullOrEmpty(shipName))
                        query = query.Where(x => x.Ship.Name.Contains(shipName));

                    if (!string.IsNullOrEmpty(shift) && Enum.TryParse<ShiftType>(shift, ignoreCase: true, out var shiftEnum))
                        query = query.Where(x => x.Shift == shiftEnum);

                    if (!string.IsNullOrEmpty(zone) && Enum.TryParse<ZoneType>(zone, ignoreCase: true, out var zoneEnum))
                        query = query.Where(x => x.Zone == zoneEnum);

                    if (date.HasValue)
                        query = query.Where(x => x.Date == date.Value);

                    var totalCount = await query.CountAsync(token);

                    // Sorting
                    query = paginationParams.SortBy?.ToLower() switch
                    {
                        "date" => paginationParams.IsDescending
                            ? query.OrderByDescending(x => x.Date)
                            : query.OrderBy(x => x.Date),
                        "ship" => paginationParams.IsDescending
                            ? query.OrderByDescending(x => x.Ship.Name)
                            : query.OrderBy(x => x.Ship.Name),
                        _ => query.OrderByDescending(x => x.Date)
                    };

                    // Pagination
                    var items = await query
                        .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                        .Take(paginationParams.PageSize)
                        .ToListAsync(token);

                    return new PagedResult<TallySheet>
                    {
                        Items = items,
                        TotalCount = totalCount,
                        PageNumber = paginationParams.PageNumber,
                        PageSize = paginationParams.PageSize
                    };
                },
                TimeSpan.FromMinutes(10),
                tags: ["tallySheets"]
            );

        }

        public async Task<TallySheet?> GetOneAsync(int id)
        {
            var key = $"tallySheet_{id}";

            return await caching.GetOrSetAsync(
                key,
                async token =>
                {
                    var tallySheet = await context.TallySheets
                        .AsNoTracking()
                        .Include(x => x.Ship)
                        .FirstOrDefaultAsync(x => x.Id == id, token);
                    return tallySheet;
                },
                TimeSpan.FromMinutes(10)
            );
        }

        public async Task<List<TallySheet>> GetTallySheetsByShip(int shipId)
        {
            var tallySheets = await context.TallySheets
                        .Where(x => x.ShipId == shipId)
                        .ToListAsync();
            return tallySheets;
        }

        public async Task<bool?> UpdateOneAsync(int id, int TeamsCount, ShiftType Shift, ZoneType Zone, int ShipId)
        {
            var tallySheet = await context.TallySheets
                        .Where(x => x.Id == id)
                        .ExecuteUpdateAsync(
                            x => x.SetProperty(p => p.TeamsCount, TeamsCount)
                                .SetProperty(p => p.Shift, Shift)
                                .SetProperty(p => p.Zone, Zone)
                                .SetProperty(p => p.ShipId, ShipId));
            if (tallySheet == 0) return null;
            return true;
        }
    }
}