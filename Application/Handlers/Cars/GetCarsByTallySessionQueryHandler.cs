using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Cars;
using Application.Mappers;
using Application.Queries.Cars;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Cars
{
    public class GetCarsByTallySessionQueryHandler(ICars carsService)
        : IRequestHandler<GetAllCarsByTallySeesionQuery, List<CarsDto>>
    {
        private readonly ICars _carsService = carsService;

        public async Task<List<CarsDto>> Handle(
            GetAllCarsByTallySeesionQuery request,
            CancellationToken cancellationToken
        )
        {
            var result = await _carsService.GetAllCarsByTallySession(request.TallySessionId);
            var response = result.Select(e => e.MapToDto()).ToList();
            return response;
        }
    }
}
