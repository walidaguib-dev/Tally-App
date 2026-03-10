using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers.Pagination;

namespace Application.Dtos.TallySheets
{
    public class TallySheetsQueryDto : PaginationParams
    {
        public string? ShipName { get; set; }
        public string? Shift { get; set; }   // "morning", "afternoon", "night"
        public string? Zone { get; set; }    // "zone_a", "zone_b"
        public DateOnly? Date { get; set; }
    }
}