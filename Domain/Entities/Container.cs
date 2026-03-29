using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Container
    {
        public int Id { get; set; }
        public string ContainerNumber { get; set; } = null!;
        public string? Bill_of_lading { get; set; }
        public ContainerSize ContainerSize { get; set; }
        public ContainerType ContainerType { get; set; }
        public ContainerStatus ContainerStatus { get; set; }
        public string? SealNumber { get; set; }
        public string userId { get; set; } = null!;
        public User user { get; set; } = null!;
        public int TallySheetId { get; set; }
        public TallySheet TallySheet { get; set; } = null!;
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
    }
}
