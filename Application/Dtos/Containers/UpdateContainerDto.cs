using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Containers
{
    public class UpdateContainerDto
    {
        public string ContainerNumber { get; set; } = null!;
        public string? Bill_of_lading { get; set; }
        public string ContainerSize { get; set; } = string.Empty;
        public string ContainerType { get; set; } = string.Empty;
        public string ContainerStatus { get; set; } = string.Empty;
        public string? SealNumber { get; set; }
        public int ClientId { get; set; }
    }
}
