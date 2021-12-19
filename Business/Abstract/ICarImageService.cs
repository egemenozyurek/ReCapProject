using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(CarImageForAddDto carImageForAddDto);
        IResult Update(CarImageForUpdateDto carImageForUpdateDto);
        IResult Delete(int id);
        IDataResult<CarImage> GetByImageId(int imageId);
        IDataResult<List<CarImage>> GetAll();
        IDataResult<List<CarImage>> GetAllByCarId(int carId);
    }
}
