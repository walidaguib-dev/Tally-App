using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Cars;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.Cars
{
    public record CreateCarsCommand(CreateCarDto dto) : IRequest<CarsDto>, IInvalidateCache
    {
        public List<string> CacheKeys => [];

        public List<string> CacheTags => ["car", "cars"];
    }
}
