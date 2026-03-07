using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheets;
using Domain.Entities;
using MediatR;

namespace Application.Queries.TallySheets
{
    public record GetAllTallySheetsByShipQuery(int shipId) : IRequest<List<TallySheetDto>>;
}