using Application.Commands.Merchandises;
using Application.Dtos.Merchandises;
using Application.Mappers;
using Domain.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Merchandises
{
    public class UpdateMerchandiseHandler(
        IMerchandise merchandiseService
        //[FromKeyedServices("updateMerchandise")] IValidator<UpdateMerchandiseDto> validator
        ) : IRequestHandler<UpdateMerchandiseCommand, string?>
    {
        private readonly IMerchandise _merchandiseService = merchandiseService;
        //private readonly IValidator<UpdateMerchandiseDto> _validator = validator;
        public async Task<string?> Handle(UpdateMerchandiseCommand request, CancellationToken cancellationToken)
        {
            //var validationResult = await _validator.ValidateAsync(request.Dto);
            //if (!validationResult.IsValid)
            //{
            //    throw new ValidationException(validationResult.Errors);
            //}

            var result = await _merchandiseService.UpdateOne(request.Id, request.Dto.Name, request.Dto.Type);
            if (result == false) throw new Exception("merchandise not found or invalid");
            return $"merchandise is updated!";
        }
    }
}
