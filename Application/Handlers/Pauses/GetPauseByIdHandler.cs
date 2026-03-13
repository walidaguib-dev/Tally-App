using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Pauses;
using Application.Mappers;
using Application.Queries.Pauses;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Pauses
{
    public class GetPauseByIdHandler(
        IPauses pausesService
    ) : IRequestHandler<GetPauseByIdQuery, PauseDto?>
    {
        private readonly IPauses _pausesService = pausesService;
        public async Task<PauseDto?> Handle(GetPauseByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _pausesService.GetById(request.Id);
            var response = result?.ToPauseDto() ?? null;
            return response;
        }
    }
}