using Entities.DTOs;
using FluentValidation;

namespace Business.Validations.FluentValidation
{
    public class CarImageCreateDtoValidator : AbstractValidator<CarImageForAddDto>
    {
        public CarImageCreateDtoValidator()
        {
            RuleFor(x => x.CarId).NotEmpty();
            RuleFor(x => x.Image).NotEmpty();
        }
    }
}
