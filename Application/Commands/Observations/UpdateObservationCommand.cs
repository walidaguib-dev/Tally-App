using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Observations;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.Observations
{
    public record UpdateObservationCommand(int Id, UpdateObservationDto Dto)
        : IRequest<bool?>,
            IInvalidateCache
    {
        public List<string> CacheKeys => [$"observation_{Id}", $"observation_{Dto.TallySheetId}"];

        public List<string> CacheTags => ["observations", "observation"];
    }
}
