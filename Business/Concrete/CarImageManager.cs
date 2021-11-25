using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.FileManipulate;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        IFileManipulateService _fileManipulateService;

        public CarImageManager(ICarImageDal carImageDal, IFileManipulateService fileManipulateService)
        {
            _carImageDal = carImageDal;
            _fileManipulateService = fileManipulateService;
        }

        public IResult Add(CarImage carImage)
        {
            var result = BusinessRuler.Run(CheckIfCarImageCountExceed(carImage.CarId));
            if (result != null)
            {
                return result;
            }
            var info = _fileManipulateService.Add(carImage.ImagePath);

            carImage.ImagePath = info.FullName;
            carImage.Date = info.CreationTime;
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            _fileManipulateService.Delete(carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            var result = CheckIfTheCarHasAnyImage(carId);
            if (!result.Success)
            {
                return new SuccessDataResult<List<CarImage>>(new List<CarImage> { new CarImage { ImagePath = @"C:\Users\Asus\Desktop\EgemenProje(DOKUNMA!)\ReCapProject\Business\Images\CarImages\logo.jpg" } });
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == carId));
        }

        public IResult Update(CarImage carImage)
        {
            var oldCarImage = _carImageDal.Get(ci => ci.Id == carImage.Id);

            var info = _fileManipulateService.Update(oldCarImage.ImagePath, carImage.ImagePath);
            carImage.ImagePath = info.FullName;
            carImage.Date = info.CreationTime;
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

        private IResult CheckIfCarImageCountExceed(int carId)
        {
            var result = _carImageDal.GetAll(i => i.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        private IResult CheckIfTheCarHasAnyImage(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Any();
            if (!result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
}
