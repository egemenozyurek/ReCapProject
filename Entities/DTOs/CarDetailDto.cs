using Core.Entities;
using Entities.Concrete;
using System.Collections.Generic;

namespace Entities.DTOs
{
    public class CarDetailDto : IDto
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public string ModelYear { get; set; }
        public string Description { get; set; }
        public List<CarImage> Images { get; set; }
        public CarImage MainImage { get; set; }
        public decimal DailyPrice { get; set; }
    }
}
