using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheets;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.TallySheets
{
    public class DeleteTallySheetHandler
    (
        ITallySheet tallySheetService
    ) : IRequestHandler<DeleteTallySheetCommand, bool?>
    {
        private readonly ITallySheet _tallySheetService = tallySheetService;
        public Task<bool?> Handle(DeleteTallySheetCommand request, CancellationToken cancellationToken)
        {
            var result = _tallySheetService.DeleteAsync(request.id);
            return result;
        }
    }
}