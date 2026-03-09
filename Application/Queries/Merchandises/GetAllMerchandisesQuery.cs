using Application.Dtos.Merchandises;
using Domain.Helpers.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Merchandises
{
    public record GetAllMerchandisesQuery(MerchandisesQueryDto Dto) : IRequest<PagedResult<MerchandiseDto>>;
}
