using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetTrucks;
using Application.Mappers;
using Domain.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.TallySheetTrucks
{
    public class AssignTruckHandler(
        ITallySheetTruck _tallySheetTruckService
    ) : IRequestHandler<AssignTruckCommand, TallySheetTruck>
    {
        private readonly ITallySheetTruck tallySheetTruckService = _tallySheetTruckService;
        public async Task<TallySheetTruck> Handle(AssignTruckCommand request, CancellationToken cancellationToken)
        {
            var sheetTruck = request.Dto.MapToEntity();
            return await tallySheetTruckService.AssignTruckAsync(sheetTruck);
        }
    }
}