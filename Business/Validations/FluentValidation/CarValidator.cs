using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Validations.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.BrandId).NotEmpty();
            RuleFor(c => c.ColorId).NotEmpty();
            RuleFor(c => c.ModelYear).NotEmpty();
            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.Description).MinimumLength(2).WithMessage(Constants.Messages.CarMessages.CarNameMinLengthTwo);
            RuleFor(c => c.Description).MaximumLength(255).WithMessage(Constants.Messages.CarMessages.CarNameMaxLength);
            RuleFor(c => c.DailyPrice).NotEmpty();
            RuleFor(c => c.DailyPrice).GreaterThan(0).WithMessage(Constants.Messages.CarMessages.CarDailyPriceMustBeGreaterThanZero);
        }
    }
}
