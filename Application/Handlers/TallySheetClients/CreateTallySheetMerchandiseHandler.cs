using Application.Commands.TallySheetClient;
using Application.Commands.TallySheetClients;

using Application.Dtos.TallySheetClient;
using Application.Mappers;
using Domain.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.TallySheetClients
{
    public class CreateTallySheetMerchandiseHandler(ITallySheetClient _sheetClient)
        : IRequestHandler<CreateTallySheetClientCommand, TallySheetClientDto>
    {
        private readonly ITallySheetClient sheetClient = _sheetClient;

        public async Task<TallySheetClientDto> Handle(
            CreateTallySheetClientCommand request,
            CancellationToken cancellationToken
        )
        {
            var item = request.Dto.MapToEntity();
            var result = await sheetClient.AddMerchandise(item);
            return result.MapToDto();
        }
    }
}
