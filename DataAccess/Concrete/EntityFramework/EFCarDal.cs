using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCarDal : EFEntityRepositoryBase<Car, CarContext>, ICarDal
    {
        public List<CarDetailDto> GetAllByBrandId(int brandId)
        {
            using (CarContext carContext = new CarContext())
            {
                var result =
                    from c in carContext.Cars
                    join co in carContext.Colors
                        on c.ColorId equals co.Id
                    join b in carContext.Brands
                        on c.BrandId equals b.Id
                    join img in carContext.CarImages
                        on c.Id equals img.CarId
                    where c.BrandId == brandId
                    select new CarDetailDto
                    {
                        CarId = c.Id,
                        CarName = c.CarName,
                        ColorName = co.Name,
                        BrandName = b.Name,
                        DailyPrice = c.DailyPrice,
                        ModelYear = c.ModelYear,
                        Description = c.Description,
                        MainImage = img
                    };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetAllByColorId(int colorId)
        {
            using (CarContext carContext = new CarContext())
            {
                var result = from c in carContext.Cars
                             join co in carContext.Colors
                             on c.ColorId equals co.Id
                             join b in carContext.Brands
                             on c.Id equals b.Id
                             join img in carContext.CarImages
                             on c.Id equals img.CarId
                             where c.ColorId == colorId
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 CarName = c.CarName,
                                 ColorName = co.Name,
                                 BrandName = b.Name,
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 MainImage = img
                             };
                return result.ToList();
            }
        }

        public CarDetailDto GetCarDetailById(int id)
        {
            using (CarContext carContext = new CarContext())
            {
                var result =
                    from c in carContext.Cars
                    join co in carContext.Colors
                        on c.ColorId equals co.Id
                    join b in carContext.Brands
                        on c.BrandId equals b.Id
                    join img in carContext.CarImages
                        on c.Id equals img.CarId
                    where c.Id == id

                    select new CarDetailDto
                    {
                        CarId = c.Id,
                        CarName = c.CarName,
                        ColorName = co.Name,
                        BrandName = b.Name,
                        DailyPrice = c.DailyPrice,
                        ModelYear = c.ModelYear,
                        Description = c.Description,
                        Images = carContext.CarImages.Where(i => i.CarId == id).ToList(),
                        MainImage = img
                    };
                return result.FirstOrDefault();
            }
        }

        public List<CarDetailDto> GetCarDetails()
        {
            using (CarContext context = new CarContext())
            {
                var result = from c in context.Cars
                             join co in context.Colors
                             on c.ColorId equals co.Id
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             select new CarDetailDto { BrandName = c.Description, CarName = b.Name, ColorName = co.Name, DailyPrice = c.DailyPrice };
                return result.ToList();
            }
        }
    }
}
