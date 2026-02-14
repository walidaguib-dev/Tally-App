using Application.Dtos.Clients;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Clients
{
    public class CreateClientValidation : AbstractValidator<CreateClientDto>
    {
        public CreateClientValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("client name is required")
                .WithName("Client name");
            RuleFor(x => x.ContactInfo)
                .NotEmpty()
                .NotNull()
                .WithMessage("client contact information is required")
                .WithName("Client contact info");
            RuleFor(x => x.Bill_Of_Lading)
                .Must(x => x.Capacity > 0)
                .WithMessage("Bill of lading must have at least one BL")
                .WithName("B/L");
            RuleFor(x => x.MerchandiseId)
             .GreaterThan(0)
             .NotNull()
             .WithMessage("Merchandise id is required")
             .WithName("Merchandise id");

        }
    }
}
