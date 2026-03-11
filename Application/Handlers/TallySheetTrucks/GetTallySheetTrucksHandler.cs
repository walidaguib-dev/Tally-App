using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheetTrucks;
using Application.Mappers;
using Application.Queries.TallySheetTrucks;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.TallySheetTrucks
{
    public class GetTallySheetTrucksHandler(
        ITallySheetTruck _tallySheetTruckService
    ) : IRequestHandler<GetTallySheetTrucks, List<AssignedTruckDto>>
    {
        private readonly ITallySheetTruck tallySheetTruckService = _tallySheetTruckService;
        public async Task<List<AssignedTruckDto>> Handle(GetTallySheetTrucks request, CancellationToken cancellationToken)
        {
            var result = await tallySheetTruckService.GetTallySheetTrucksAsync(request.TallySheetId);
            var response = result.Select(e => e.MapToJson()).ToList();
            return response;
        }
    }
}