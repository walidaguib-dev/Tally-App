using Application.Commands.Merchandises;
using Application.Dtos.Merchandises;
using Application.Mappers;
using Domain.Contracts;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Merchandises
{
    public class CreateMerchandiseHandler(
        IMerchandise merchandiseService

        ) : IRequestHandler<CreateMerchandiseCommand, Merchandise>
    {
        private readonly IMerchandise _merchandiseService = merchandiseService;

        public async Task<Merchandise> Handle(CreateMerchandiseCommand request, CancellationToken cancellationToken)
        {
            //ValidationResult validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);
            //if(!validationResult.IsValid)
            //{
            //    throw new ValidationException(validationResult.Errors);
            //}
            var merchandise = request.Dto.MapToModel();
            var result = await _merchandiseService.CreateOne(merchandise);
            return result;
        }
    }
}
