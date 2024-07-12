using TourV2.MediatR.Commands;
using FluentValidation;

namespace TourV2.MediatR.Validators
{
    public class UserLoginCommandValidator: AbstractValidator<UserLoginCommand>
    {
        public UserLoginCommandValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage("Please enter username.");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Please enter password.");
        }
    }
}
