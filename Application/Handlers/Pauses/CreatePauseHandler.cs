using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Pauses;
using Application.Mappers;
using Domain.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.Pauses
{
    public class CreatePauseHandler(
        IPauses pausesService
    ) : IRequestHandler<CreatePauseCommand, Pause>
    {
        private readonly IPauses _pausesService = pausesService;
        public async Task<Pause> Handle(CreatePauseCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Dto.ToModel();
            return await _pausesService.CreatePause(entity);
        }
    }
}