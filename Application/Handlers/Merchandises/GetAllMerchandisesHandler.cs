using Application.Dtos.Merchandises;
using Application.Mappers;
using Application.Queries.Merchandises;
using Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Merchandises
{
    internal class GetAllMerchandisesHandler(
        IMerchandise merchandiseService
        ) : IRequestHandler<GetAllMerchandisesQuery, List<MerchandiseDto>>
    {
        private readonly IMerchandise _merchandiseService = merchandiseService;
        public async Task<List<MerchandiseDto>> Handle(GetAllMerchandisesQuery request, CancellationToken cancellationToken)
        {
            var MerchandisesList = await _merchandiseService.GetMerchandisesAsync();
            var response = MerchandisesList.Select(m => m.MapToJson()).ToList();
            return response;
        }
    }
}
