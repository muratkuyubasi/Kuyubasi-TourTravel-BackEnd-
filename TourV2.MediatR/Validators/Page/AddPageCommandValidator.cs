using TourV2.MediatR.Commands;
using FluentValidation;

namespace TourV2.MediatR.Validators
{
    public class AddPageCommandValidator:  AbstractValidator<AddPageCommand>
    {
        public AddPageCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
