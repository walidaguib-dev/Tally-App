using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Cars;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.Cars
{
    public record UpdateCarsCommand(int Id, UpdateCarDto Dto) : IRequest<bool?>, IInvalidateCache
    {
        public List<string> CacheKeys => [$"car_{Id}"];

        public List<string> CacheTags => ["car", "cars"];
    }
}
