using Entities.DTOs;
using FluentValidation;

namespace Business.Validations.FluentValidation
{
    public class CarImageForUpdateDtoValidator : AbstractValidator<CarImageForUpdateDto>
    {
        public CarImageForUpdateDtoValidator()
        {
            RuleFor(x => x.CarId).NotEmpty();
            RuleFor(x => x.Image).NotEmpty();
            RuleFor(x => x.File).NotEmpty();
        }
    }
}
