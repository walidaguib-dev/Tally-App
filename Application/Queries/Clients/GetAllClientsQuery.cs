using Application.Dtos.Clients;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Helpers.Pagination;

namespace Application.Queries.Clients
{
    public record GetAllClientsQuery(ClientsQueryDto dto) : IRequest<PagedResult<ClientsDto>>;
}
