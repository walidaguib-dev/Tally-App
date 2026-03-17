using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Observations;
using Application.Dtos.Observations;
using Application.Mappers;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Observations
{
    public class CreateObservationsHandler(IObservations observationsService)
        : IRequestHandler<CreateObservationCommand, ObservationsDto>
    {
        private readonly IObservations _observationsService = observationsService;

        public async Task<ObservationsDto> Handle(
            CreateObservationCommand request,
            CancellationToken cancellationToken
        )
        {
            var item = request.Dto.MapToEntity();
            var result = await _observationsService.CreateOne(item);
            return result.MapToDto();
        }
    }
}
