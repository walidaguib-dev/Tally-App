using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Observations;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.Observations
{
    public record CreateObservationCommand(CreateObservationDto Dto)
        : IRequest<ObservationsDto>,
            IInvalidateCache
    {
        public List<string> CacheKeys => [$"observation_{Dto.TallySheetId}"];

        public List<string> CacheTags => ["observations", "observation"];
    }
}
