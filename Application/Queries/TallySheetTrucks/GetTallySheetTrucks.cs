using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheetTrucks;
using MediatR;

namespace Application.Queries.TallySheetTrucks
{
    public record GetTallySheetTrucks(int TallySheetId) : IRequest<List<AssignedTruckDto>>;
}