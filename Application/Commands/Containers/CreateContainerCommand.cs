using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Containers;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.Containers
{
    public record CreateContainerCommand(CreateContainerDto Dto)
        : IRequest<ContainersDto>,
            IInvalidateCache
    {
        public List<string> CacheKeys => [$"containers_{Dto.TallySheetId}"];

        public List<string> CacheTags => ["containers", "container"];
    }
}
