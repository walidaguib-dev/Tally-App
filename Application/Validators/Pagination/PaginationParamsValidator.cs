using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers.Pagination;
using FluentValidation;

namespace Application.Validators.Pagination
{
    public class PaginationParamsValidator : AbstractValidator<PaginationParams>
    {
        public PaginationParamsValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be at least 1.")
                .WithName("PageNumber");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("Page size must be at least 1.")
                .LessThanOrEqualTo(100)
                .WithMessage("Page size cannot exceed 100.")
                .WithName("PageSize");
            RuleFor(x => x.SortBy)
                .NotNull()
                .NotNull()
                .WithMessage("SortBy must be 'name' or 'id'.")
                .WithName("SortBy");
            RuleFor(x => x.IsDescending)
                .NotNull()
                .WithMessage("IsDescending must be a boolean value.")
                .WithName("IsDescending");
        }
    }
}
