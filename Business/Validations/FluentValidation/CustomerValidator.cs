using Entities.Concrete;
using FluentValidation;

namespace Business.Validations.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(c => c.CompanyName).MinimumLength(2).WithMessage(Constants.Messages.CustomerMessages.CustomerCompanyNameMinLength);
            RuleFor(c => c.CompanyName).MaximumLength(255).WithMessage(Constants.Messages.CustomerMessages.CustomerCompanyNameMaxLength);
        }
    }
}
