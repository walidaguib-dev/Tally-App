using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Cars;
using MediatR;

namespace Application.Queries.Cars
{
    public record GetCarQuery(int Id) : IRequest<CarsDto?> { }
}
