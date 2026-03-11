using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetTrucks;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.TallySheetTrucks
{
    public class EndTruckTimeHandler(
        ITallySheetTruck _tallySheetTruckService
    ) : IRequestHandler<EndTruckTimeCommand, bool?>
    {
        private readonly ITallySheetTruck tallySheetTruckService = _tallySheetTruckService;
        public async Task<bool?> Handle(EndTruckTimeCommand request, CancellationToken cancellationToken)
        {
            var (Id, Dto) = request;
            return await tallySheetTruckService.EndTruckSessionTime(Id, Dto.EndTime);
        }
    }
}