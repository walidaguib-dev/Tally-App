using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers.Pagination;

namespace Application.Dtos.Merchandises
{
    public class MerchandisesQueryDto : PaginationParams
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
    }
}