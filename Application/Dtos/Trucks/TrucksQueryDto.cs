using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers.Pagination;

namespace Application.Dtos.Trucks
{
    public class TrucksQueryDto : PaginationParams
    {
        public string? PlateNumber { get; set; }
    }
}