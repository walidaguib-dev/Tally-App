using Application.Commands.Merchandises;
using Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Merchandises
{
    public class DeleteMerchandiseHandler(
        IMerchandise merchandiseService
        ) : IRequestHandler<DeleteMerchandiseCommand, string?>
    {
        private readonly IMerchandise _merchandiseService = merchandiseService;
        public async Task<string?> Handle(DeleteMerchandiseCommand request, CancellationToken cancellationToken)
        {
            var result = await _merchandiseService.DeleteOne(request.Id);
            if (result == false) throw new Exception($"merchandise-{request.Id} not found!");
            return "merchandise is deleted!";
        }
    }
}
