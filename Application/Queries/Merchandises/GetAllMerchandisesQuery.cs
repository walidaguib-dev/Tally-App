using Application.Dtos.Merchandises;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Merchandises
{
    public record GetAllMerchandisesQuery() : IRequest<List<MerchandiseDto>>;
}
