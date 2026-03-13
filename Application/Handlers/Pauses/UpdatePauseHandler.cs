using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Pauses;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Pauses
{
    public class UpdatePauseHandler(
        IPauses pausesService
    ) : IRequestHandler<UpdatePauseCommand, bool?>
    {
        private readonly IPauses _pausesService = pausesService;
        public async Task<bool?> Handle(UpdatePauseCommand request, CancellationToken cancellationToken)
        {
            return await _pausesService.UpdatePause(request.id, new UpdatePauseObject
            {
                StartTime = request.Dto.StartTime,
                Reason = request.Dto.Reason,
                Notes = request.Dto.Notes,
                TruckId = request.Dto.TruckId
            });
        }
    }
}