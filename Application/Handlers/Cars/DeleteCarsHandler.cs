using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Cars;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Cars
{
    public class DeleteCarsHandler(ICars carsService) : IRequestHandler<DeleteCarsCommand, bool?>
    {
        private readonly ICars _carsService = carsService;

        public async Task<bool?> Handle(
            DeleteCarsCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _carsService.DeleteOne(request.Id);
        }
    }
}
