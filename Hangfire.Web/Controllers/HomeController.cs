using Hangfire.Web.BackgroudJobs;
using Hangfire.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hangfire.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        public IActionResult SignUp()
        {
            FireAndForgetJobs.EmailSendToUserJob("anilorhan-@hotmail.com","Kayýt baþarýlý, hoþgeldiniz!","Tebrikler kaydýnýz baþarýyla gerçekleþmiþtir.");

            return View();

        }

        public IActionResult PictureSave()
        {
            BackgroudJobs.ReccurringJobs.ReportingJob();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PictureSave(IFormFile picture)
        {
            string fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pictures", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await picture.CopyToAsync(stream);
            }

            var jobID =  DelayedJobs.AddWatermarkJob(fileName, "www.anilorhan.dev");

            BackgroudJobs.ContinuationsJobs.WriteLogJob(jobID, $"{fileName} isimli resme watermark eklendi.");


            return View();

        }
    }
}
