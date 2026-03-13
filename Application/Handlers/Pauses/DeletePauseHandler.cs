using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Pauses;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Pauses
{
    public class DeletePauseHandler(
        IPauses pausesService
    ) : IRequestHandler<DeletePauseCommand, bool?>
    {
        private readonly IPauses _pausesService = pausesService;
        public async Task<bool?> Handle(DeletePauseCommand request, CancellationToken cancellationToken)
        {
            return await _pausesService.DeletePause(request.Id);
        }
    }
}