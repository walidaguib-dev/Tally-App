using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Cars;
using Application.Mappers;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Cars
{
    public class UpdateCarsHandler(ICars carsService) : IRequestHandler<UpdateCarsCommand, bool?>
    {
        private readonly ICars _carsService = carsService;

        public async Task<bool?> Handle(
            UpdateCarsCommand request,
            CancellationToken cancellationToken
        )
        {
            var updateRequest = request.Dto.MapToUpdateDto();
            return await _carsService.UpdateOne(request.Id, updateRequest);
        }
    }
}
