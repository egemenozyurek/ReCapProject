using Entities.Concrete;
using FluentValidation;

namespace Business.Validations.FluentValidation
{
    public class ColorValidator : AbstractValidator<Color>
    {
        public ColorValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(2).WithMessage(Constants.Messages.ColorMessages.ColorNameMinLength);
            RuleFor(x => x.Name).MaximumLength(255).WithMessage(Constants.Messages.ColorMessages.ColorNameMaxLength);
        }
    }
}
