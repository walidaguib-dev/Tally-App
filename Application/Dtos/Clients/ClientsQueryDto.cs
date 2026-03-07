using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers.Pagination;


namespace Application.Dtos.Clients
{
    public class ClientsQueryDto : PaginationParams
    {
        public string? Name { get; set; } = null;
    }
}