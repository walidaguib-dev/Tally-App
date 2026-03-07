using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers.Pagination;

namespace Application.Dtos.Ships
{
    public class ShipsQueryDto : PaginationParams
    {
        public string? Name { get; set; }

    }
}