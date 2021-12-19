using Entities.Concrete;
using FluentValidation;

namespace Business.Validations.FluentValidation
{
    public class ColorValidator : AbstractValidator<Color>
    {
        public ColorValidator()
        {
            RuleFor(c => c.Name).NotNull();
            RuleFor(co => co.Name).MinimumLength(2).WithMessage("Renk Adı Minimum 2 Harfli Olmalıdır.");
        }
    }
}
