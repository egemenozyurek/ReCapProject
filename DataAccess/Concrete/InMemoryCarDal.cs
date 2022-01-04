using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{Id=1,BrandId=1,DailyPrice=400,ModelYear="2015",Description="BMW-SUV"},
                new Car{Id=2,BrandId=2,DailyPrice=350,ModelYear="2016",Description="Mercedes-Sedan"},
                new Car{Id=3,BrandId=3,DailyPrice=290,ModelYear="2019",Description="Renault-SUV"},
                new Car{Id=4,BrandId=4,DailyPrice=170,ModelYear="2011",Description="Dacia-SUV"},
                new Car{Id=5,BrandId=5,DailyPrice=150,ModelYear="2009",Description="Nissan-Sedan"},
            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public Task AddAsync(Car car)
        {
            throw new NotImplementedException();
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(x => x.Id == car.Id);

            _cars.Remove(carToDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            return _cars.FirstOrDefault();
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            return _cars;
        }

        public Task<List<Car>> GetAllAsync(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<Car> GetAsync(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public CarDetailDto GetCarDetailById(Expression<Func<CarDetailDto, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<CarDetailDto> GetCarDetailByIdAsync(Expression<Func<CarDetailDto, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public Task<List<CarDetailDto>> GetCarDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(x => x.Id == car.Id);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.Description = car.Description;
        }

        public Task UpdateAsync(Car entity)
        {
            throw new NotImplementedException();
        }
    }
}