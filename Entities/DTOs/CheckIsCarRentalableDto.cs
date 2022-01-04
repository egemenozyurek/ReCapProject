using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CheckIsCarRentalableDto : IDto
    {
        public int CarId { get; set; }
        public DateTime RentDate { get; set; }
    }
}
