using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Cars;
using Application.Dtos.Cars;
using Application.Mappers;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Cars
{
    public class CreateCarsHandler(ICars carsService) : IRequestHandler<CreateCarsCommand, CarsDto>
    {
        private readonly ICars _carsService = carsService;

        public async Task<CarsDto> Handle(
            CreateCarsCommand request,
            CancellationToken cancellationToken
        )
        {
            var item = request.dto.MapToEntity();
            await _carsService.CreateOne(item);
            return item.MapToDto();
        }
    }
}
