using Application.Dtos.Trucks;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Trucks
{
    public class UpdateTruckValidation : AbstractValidator<UpdatetTruckDto>
    {
        public UpdateTruckValidation()
        {
            RuleFor(x => x.PlateNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("Name is required!")
                .WithName("Truck name");
            RuleFor(x => x.Capacity)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Capacity is required!")
                .WithName("Truck capacity");
        }
    }
}
