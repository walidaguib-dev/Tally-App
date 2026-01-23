using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.helpers
{
    public record ValidationErrorResponse
    {
        public IEnumerable<string> Errors { get; set; } = [];
    }
}