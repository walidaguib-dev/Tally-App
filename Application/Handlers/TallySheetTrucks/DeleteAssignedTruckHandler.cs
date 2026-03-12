using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetTrucks;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.TallySheetTrucks
{
    public class DeleteAssignedTruckHandler(
        ITallySheetTruck _tallySheetTruckService
    ) : IRequestHandler<DeleteAssignedTruckCommand, bool?>
    {
        private readonly ITallySheetTruck tallySheetTruckService = _tallySheetTruckService;
        public async Task<bool?> Handle(DeleteAssignedTruckCommand request, CancellationToken cancellationToken)
        {
            return await tallySheetTruckService.DeleteAssignedTruck(request.TallySheetId, request.TruckId);
        }
    }
}