using TourV2.MediatR.Commands;
using FluentValidation;

namespace TourV2.MediatR.Validators
{
    public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Role Name is required.");
        }
    }
}
