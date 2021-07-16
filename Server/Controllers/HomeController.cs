using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
        public HomeController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        // Отправка потока
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Stream")]
        public ActionResult SendStream()
        {
            var path = Path.Combine(_appEnvironment.ContentRootPath, "Files/Example.txt");
            // Открываем поток
            var stream = System.IO.File.OpenRead(path);

            return new FileStreamResult(stream, "application/txt")
            {
                FileDownloadName = "Example.txt"
            };
        }
    }
}
