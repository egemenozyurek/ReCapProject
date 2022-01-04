using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BrandValidator : AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(2).WithMessage(Constants.Messages.BrandMessages.BrandNameMinLength);
            RuleFor(x => x.Name).MaximumLength(255).WithMessage(Constants.Messages.BrandMessages.BrandNameMaxLength);
        }
    }
}