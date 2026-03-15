using Application.Commands.TallySheetMerchandise;
using Application.Dtos.TallySheetMerchandise;
using Application.Mappers;
using Domain.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.TallySheetMerchadises
{
    public class CreateTallySheetMerchandiseHandler(
        ITallySheetMerchandise _sheetMerchandiseService
    ) : IRequestHandler<CreateTallySheetMerchandiseCommand, TallySheetMerchandiseDto>
    {
        private readonly ITallySheetMerchandise sheetMerchandise = _sheetMerchandiseService;
        public async Task<TallySheetMerchandiseDto> Handle(CreateTallySheetMerchandiseCommand request, CancellationToken cancellationToken)
        {
            var item = request.Dto.MapToEntity();
            var result = await sheetMerchandise.AddMerchandise(item);
            return result.MapToDto();
        }
    }
}