using Entities.Concrete;
using FluentValidation;

namespace Business.Validations.FluentValidation
{
    public class CarImagesValidator : AbstractValidator<CarImage>
    {
        public CarImagesValidator()
        {
            RuleFor(c => c.CarId).NotEmpty();
        }
    }
}
