using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Pauses;
using Domain.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.Pauses
{
    public class EndPauseTimeHandler(
        IPauses pausesService
    ) : IRequestHandler<EndPauseCommand, bool?>
    {
        private readonly IPauses _pausesService = pausesService;
        public async Task<bool?> Handle(EndPauseCommand request, CancellationToken cancellationToken)
        {
            return await _pausesService.EndPause(request.Id, request.Dto.EndTime);
        }
    }
}