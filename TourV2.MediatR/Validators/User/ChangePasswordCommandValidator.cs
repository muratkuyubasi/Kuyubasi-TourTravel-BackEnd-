using TourV2.MediatR.Commands;
using FluentValidation;

namespace TourV2.MediatR.Validators
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage("Username is required.");
            RuleFor(c => c.OldPassword).NotEmpty().WithMessage("Old Password is required.");
            RuleFor(c => c.NewPassword).NotEmpty().WithMessage("New Password is required.");
        }
    }
}
