using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpPost("add")]
        public void Add([FromForm]IFormFile formFile)
        {
            string path = @"C:\Users\Asus\Desktop\EgemenProje(DOKUNMA!)\ReCapProject\Business\Images\CarImages\lamborghini.jpg";
            FileStream stream = new FileStream(path, FileMode.Create);

            formFile.CopyTo(stream);
            stream.Close();
        }
    }
}
