using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Observations;
using Application.Mappers;
using Application.Queries.Observations;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Observations
{
    public class GetAllObservationsByTallyIdHandler(IObservations observationsService)
        : IRequestHandler<GetAllObservationByTallyIdQuery, List<ObservationsDto>>
    {
        private readonly IObservations _observationsService = observationsService;

        public async Task<List<ObservationsDto>> Handle(
            GetAllObservationByTallyIdQuery request,
            CancellationToken cancellationToken
        )
        {
            var result = await _observationsService.GetAllByTallyId(request.TallySheetId);
            var response = result.Select(e => e.MapToDto()).ToList();
            return response;
        }
    }
}
