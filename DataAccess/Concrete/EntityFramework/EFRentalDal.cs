using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EFEntityRepositoryBase<Rental, CarContext>, IRentalDal
    {
        public Rental GetLastRentalByCarId(int carId)
        {
            using (CarContext context = new CarContext())
            {
                return context.Rentals.Where(r => r.CarId == carId).OrderByDescending(o => o.Id).FirstOrDefault();
            }
        }

        public async Task<Rental> GetLastRentalByCarIdAsync(int carId)
        {
            using (CarContext context = new CarContext())
            {
                return await context.Rentals.Where(r => r.CarId == carId).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            }
        }

        public async Task<List<RentalDetailDto>> GetRentalDetailsAsync()
        {
            using (CarContext context = new CarContext())
            {
                return await GetRentalDetailsQuery(context).ToListAsync();
            }
        }

        public List<RentalDetailDto> GetRentalDetails()
        {
            using (CarContext context = new CarContext())
            {
                return GetRentalDetailsQuery(context).ToList();
            }
        }

        private IQueryable<RentalDetailDto> GetRentalDetailsQuery(CarContext context)
        {
            return from rental in context.Rentals.AsNoTracking()
                   join car in context.Cars.AsNoTracking() on rental.CarId equals car.Id
                   join brand in context.Brands.AsNoTracking() on car.BrandId equals brand.Id
                   join customer in context.Customers.AsNoTracking() on rental.CustomerId equals customer.Id
                   join user in context.Users.AsNoTracking() on customer.UserId equals user.Id
                   select new RentalDetailDto
                   {
                       Id = rental.Id,
                       BrandName = brand.Name,
                       FullName = user.FirstName + " " + user.LastName,
                       RentDate = rental.RentDate,
                       ReturnDate = rental.ReturnDate
                   };
        }
    }
}