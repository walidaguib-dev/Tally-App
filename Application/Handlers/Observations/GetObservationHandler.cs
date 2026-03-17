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
    public class GetObservationHandler(IObservations observationsService)
        : IRequestHandler<GetObservationByIdQuery, ObservationsDto?>
    {
        private readonly IObservations _observationsService = observationsService;

        public async Task<ObservationsDto?> Handle(
            GetObservationByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            var result = await _observationsService.GetById(request.Id);
            return result?.MapToDto();
        }
    }
}
