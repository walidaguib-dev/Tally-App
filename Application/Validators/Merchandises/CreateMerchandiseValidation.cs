using Application.Commands.Merchandises;
using Application.Dtos.Merchandises;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Merchandises
{
    public class CreateMerchandiseValidation : AbstractValidator<CreateMerchandiseCommand>
    {
        public CreateMerchandiseValidation()
        {
            RuleFor(x => x.Dto.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Merchandise Name is required!").WithName("Merchandise Name");
            RuleFor(x => x.Dto.Type)
                .NotEmpty()
                .NotNull()
                .WithMessage("Merchandise Type is required!").WithName("Merchandise Type");
        }
    }
}
