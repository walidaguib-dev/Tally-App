using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.Observations
{
    public record DeleteObservationCommand(int Id) : IRequest<bool?>, IInvalidateCache
    {
        public List<string> CacheKeys => [$"observation_{Id}"];

        public List<string> CacheTags => ["observations", "observation"];
    }
}
