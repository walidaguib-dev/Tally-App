using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Teams.Members;
using FluentValidation;

namespace Application.Validators.Teams.Membres
{
    public class AddToMemberValidator : AbstractValidator<CreateTeamMemberCommand>
    {
        public AddToMemberValidator()
        {
            RuleFor(x => x.Dto.UserId).NotEmpty().WithMessage("User Id is required.");
            RuleFor(x => x.Dto.TeamId).GreaterThan(0).NotNull().WithMessage("team id is required.");
            RuleFor(x => x.Dto.Role).NotEmpty().NotNull().WithMessage("Role is required.");
        }
    }
}
