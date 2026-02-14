using Application.Dtos.Clients;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Clients
{
    public record GetClientQuery(int id) : IRequest<ClientsDto?>;
}
