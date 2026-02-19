using Application.Dtos.Trucks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Trucks
{
    public record GetAllTrucksQuery : IRequest<List<TruckDto>>;
}
