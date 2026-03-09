using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers.Pagination;

namespace Application.Mappers
{
    public static class PaginationMapper
    {
        public static PagedResult<TDto> MapToPagedResult<TEntity, TDto>(
            this PagedResult<TEntity> result,
            Func<TEntity, TDto> mapper)
        {
            return new PagedResult<TDto>
            {
                Items = result.Items.Select(mapper).ToList(),
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }
    }
}