using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Observations;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Observations
{
    public class DeleteObservationsHandler(IObservations observationsService)
        : IRequestHandler<DeleteObservationCommand, bool?>
    {
        private readonly IObservations _observationsService = observationsService;

        public async Task<bool?> Handle(
            DeleteObservationCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _observationsService.DeleteOne(request.Id);
        }
    }
}
