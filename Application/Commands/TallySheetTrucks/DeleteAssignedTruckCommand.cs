using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.TallySheetTrucks
{
    public record DeleteAssignedTruckCommand(int TallySheetId, int TruckId) : IRequest<bool?>, IInvalidateCache
    {
        public List<string> CacheKeys => [$"TallySheetTrucks_{TallySheetId}"];

        public List<string> CacheTags => [];
    }
}