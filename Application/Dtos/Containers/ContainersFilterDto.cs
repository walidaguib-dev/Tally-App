using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers.Pagination;

namespace Application.Dtos.Containers
{
    public class ContainersFilterDto : PaginationParams
    {
        public string? ContainerNumber { get; set; } = string.Empty;
        public string? Bill_of_lading { get; set; }
        public string? RecordedBy { get; set; }
        public string? ClientName { get; set; }
    }
}
