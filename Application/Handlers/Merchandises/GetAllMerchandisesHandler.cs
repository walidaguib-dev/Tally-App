using Application.Dtos.Merchandises;
using Application.Mappers;
using Application.Queries.Merchandises;
using Domain.Contracts;
using Domain.Entities;
using Domain.Helpers.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Merchandises
{
    internal class GetAllMerchandisesHandler(
        IMerchandise merchandiseService
        ) : IRequestHandler<GetAllMerchandisesQuery, PagedResult<MerchandiseDto>>
    {
        private readonly IMerchandise _merchandiseService = merchandiseService;
        public async Task<PagedResult<MerchandiseDto>> Handle(GetAllMerchandisesQuery request, CancellationToken cancellationToken)
        {
            var result = await _merchandiseService.GetMerchandisesAsync(request.Dto, request.Dto.Name, request.Dto.Type);
            return result?.MapToPagedResult<Merchandise, MerchandiseDto>(m => m.MapToJson()) ?? new PagedResult<MerchandiseDto>();
        }
    }
}
