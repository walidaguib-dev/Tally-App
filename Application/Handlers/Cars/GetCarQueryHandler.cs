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
    public class GetCarQueryHandler(ICars carsService) : IRequestHandler<GetCarQuery, CarsDto?>
    {
        private readonly ICars _carsService = carsService;

        public async Task<CarsDto?> Handle(GetCarQuery request, CancellationToken cancellationToken)
        {
            var result = await _carsService.GetCarAsync(request.Id);
            var response = result?.MapToDto();
            return response;
        }
    }
}
