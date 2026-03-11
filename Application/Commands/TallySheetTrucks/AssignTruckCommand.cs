using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheetTrucks;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TallySheetTrucks
{
    public record AssignTruckCommand(AssignTruckDto Dto) : IRequest<TallySheetTruck>;
}