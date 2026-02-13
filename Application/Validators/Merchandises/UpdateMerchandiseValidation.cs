using Application.Dtos.Merchandises;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Merchandises
{
    public class UpdateMerchandiseValidation : AbstractValidator<UpdateMerchandiseDto>
    {
        public UpdateMerchandiseValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Merchandise Name is required!");
            RuleFor(x => x.Type)
                .NotEmpty()
                .NotNull()
                .WithMessage("Merchandise Type is required!");
        }
    }
}
