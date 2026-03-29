using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.Containers
{
    public record DeleteContainerCommand(string ContainerNumber) : IRequest<bool?>, IInvalidateCache
    {
        public List<string> CacheKeys => [$"container_{ContainerNumber}"];

        public List<string> CacheTags => ["containers", "container"];
    }
}
