using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Containers;
using MediatR;

namespace Application.Queries.Containers
{
    public record GetContainerQuery(string ContainerNumber) : IRequest<ContainersDto?> { }
}
