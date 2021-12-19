using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IResult Add(Rental user);
        IResult Update(Rental user);
        IResult Delete(Rental user);
        IDataResult<List<Rental>> GetAll();
        IDataResult<List<Rental>> GetById(int id);
        IDataResult<List<CarRentalDetailDto>> GetRentalCarDetails();
        IResult CheckReturnDate(int carId);
        IResult UpdateReturnDate(Rental rental);
        IResult TransactionalOperation(Rental rental);
    }
}
