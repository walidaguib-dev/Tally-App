using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Observations;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Observations
{
    public class UpdateObservationsHandler(IObservations observationsService)
        : IRequestHandler<UpdateObservationCommand, bool?>
    {
        private readonly IObservations _observationsService = observationsService;

        public async Task<bool?> Handle(
            UpdateObservationCommand request,
            CancellationToken cancellationToken
        )
        {
            var (Id, Dto) = request;
            return await _observationsService.UpdateOne(
                Id,
                new Domain.Requests.UpdateObservationRequest
                {
                    Type = Dto.Type,
                    Description = Dto.Description,
                    Timestamp = Dto.Timestamp,
                    TallySheetId = Dto.TallySheetId,
                    ClientId = Dto.ClientId,
                    TruckId = Dto.TruckId,
                }
            );
        }
    }
}
