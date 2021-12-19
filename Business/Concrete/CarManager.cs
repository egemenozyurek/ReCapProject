using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants.Messages;
using Business.Validations.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        IBrandService _brandService;
        IColorService _colorService;

        public CarManager(ICarDal carDal, IBrandService brandService, IColorService colorService)
        {
            _colorService = colorService;
            _brandService = brandService;
            _carDal = carDal;

        }
        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetAll()
        {
            if (DateTime.Now.Hour == 1)
            {
                return new ErrorDataResult<List<CarDetailDto>>(CarMessages.MaintenanceTime);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), CarMessages.CarListed);
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == carId));
        }
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]

        public IResult Add(Car car)
        {
            //BUSINESS CODE
            //İŞ KODLARI BURDA YAZILIR
            IResult result = BusinessRuler.Run(
                CheckIfCarCountBrandCorrect(car.BrandId),
                CheckIfCarNameExists(car.CarName),
                CheckIfBrandLimitExceded(),
                CheckIfColorCountLimitExceded()

            );
            _carDal.Add(car);
            return new SuccessResult(CarMessages.CarAdded);
        }
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(CarMessages.CarDeleted);
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Car car)
        {
            _carDal.Update(car);
            _carDal.Add(car);
            return new SuccessResult(CarMessages.CarUpdate);

        }
        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(CarMessages.CarUpdate);
        }

        private IResult CheckIfCarCountBrandCorrect(int brandId)
        {
            var result = _carDal.GetAll(c => c.BrandId == brandId).Count;
            if (result >= 15)
            {
                return new ErrorResult(CarMessages.CarCountOfCategoryError);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCarNameExists(string carName)
        {
            var result = _carDal.GetAll(c => c.CarName == carName).Any();
            if (result)
            {
                return new ErrorResult(CarMessages.CarNameAlreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfBrandLimitExceded()
        {
            var result = _brandService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        private IResult CheckIfColorCountLimitExceded()
        {
            var result = _colorService.GetAll();
            if (result.Data.Count > 30)
            {
                return new ErrorResult(CarMessages.ColorCountLimitExceded);
            }
            return new SuccessResult();
        }

        public IDataResult<List<CarDetailDto>> GetAllByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllByBrandId(brandId));
        }

        public IDataResult<List<CarDetailDto>> GetAllByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllByColorId(colorId));
        }


        public IDataResult<CarDetailDto> GetCarDetailById(int id)
        {
            var result = _carDal.Get(c => c.Id == id);
            if (result == null)
            {
                return new ErrorDataResult<CarDetailDto>();
            }
            return new SuccessDataResult<CarDetailDto>(_carDal.GetCarDetailById(id));
        }
    }
}
