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

    public class GetMerchandiseHandler(
        IMerchandise  merchandiseService
        ) : IRequestHandler<GetMerchandiseQuery, MerchandiseDto?>
    {
        private readonly IMerchandise _merchandiseService = merchandiseService;
    public async Task<MerchandiseDto?> Handle(GetMerchandiseQuery request, CancellationToken cancellationToken)
        {
            var result = await _merchandiseService.GetOneAsync(request.Id);
            if (result == null) throw new Exception("merchandise not found!");
            return result.MapToJson();
        }
    }
}
