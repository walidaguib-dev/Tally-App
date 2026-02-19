using Application.Dtos.Trucks;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Trucks
{
    public class CreateTruckValidation : AbstractValidator<CreateTruckDto>
    {
        public CreateTruckValidation()
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
