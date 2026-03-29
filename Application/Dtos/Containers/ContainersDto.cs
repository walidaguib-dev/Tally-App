using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Containers
{
    public class ContainersDto
    {
        public int Id { get; set; }
        public string ContainerNumber { get; set; } = null!;
        public string? Bill_of_lading { get; set; }
        public string ContainerSize { get; set; } = string.Empty;
        public string ContainerType { get; set; } = string.Empty;
        public string ContainerStatus { get; set; } = string.Empty;
        public string? SealNumber { get; set; }
        public string userId { get; set; } = null!;
        public string Username { get; set; } = null!;
        public int TallySheetId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = null!;
    }
}
