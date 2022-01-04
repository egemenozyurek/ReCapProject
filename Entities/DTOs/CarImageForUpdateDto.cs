using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.DTOs
{
    public class CarImageForUpdateDto : IDto
    {
        public int CarId { get; set; }
        public string Image { get; set; }
        public IFormFile File { get; set; }
    }
}
