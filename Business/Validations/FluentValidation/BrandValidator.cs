using Entities.Concrete;
using FluentValidation;

namespace Business.Validations.FluentValidation
{
    public class BrandValidator:AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.Name).NotEmpty();
            RuleFor(b => b.Name).MinimumLength(3).WithMessage("Marka adı en az 3 karakter uzunluğunda olmalıdır.");
        }
    }
}
