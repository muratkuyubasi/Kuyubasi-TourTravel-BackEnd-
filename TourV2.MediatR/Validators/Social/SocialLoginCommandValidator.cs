using FluentValidation;
using TourV2.MediatR.Commands;

namespace TourV2.MediatR.Validators
{
    public class SocialLoginCommandValidator : AbstractValidator<SocialLoginCommand>
    {
        public SocialLoginCommandValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage("Please enter username.");
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("Please enter firstname.");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("Please enter lastname.");
            RuleFor(c => c.Provider).NotEmpty().WithMessage("Please enter login provider name.");
            RuleFor(c => c.Email).NotEmpty().WithMessage("Please enter email .");
            RuleFor(c => c.Email).EmailAddress().WithMessage("Email in right format.");
        }
    }
}
