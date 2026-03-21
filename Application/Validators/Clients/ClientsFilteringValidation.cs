using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Clients;
using Application.Validators.Pagination;
using FluentValidation;

namespace Application.Validators.Clients
{
    public class ClientsFilteringValidation : AbstractValidator<ClientsQueryDto>
    {
        public ClientsFilteringValidation()
        {
            Include(new PaginationParamsValidator());
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Name is too long.")
                .When(x => x.Name is not null)
                .WithName("Name");
        }
    }
}
