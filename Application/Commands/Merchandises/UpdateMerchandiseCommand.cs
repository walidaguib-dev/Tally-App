using Application.Dtos.Merchandises;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Merchandises
{
    public record UpdateMerchandiseCommand(int Id , UpdateMerchandiseDto Dto) : IRequest<string?>;
}
