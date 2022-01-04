using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.DTOs
{
    public class CarImageForAddDto : IDto
    {
        public int CarId { get; set; }
        public IFormFile File { get; set; }
    }
}
