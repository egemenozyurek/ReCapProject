using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        List<CarDetailDto> GetCarDetails();
        CarDetailDto GetCarDetailById(int id);
        List<CarDetailDto> GetAllByColorId(int colorId);
        List<CarDetailDto> GetAllByBrandId(int brandId);
    }
}
