using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using techtasticwelcome.Models;
using techtasticwelcome.Services;

namespace techtasticwelcome.Controllers
{
    [Route("[controller]/[action]")]
    public class ImagesController : Controller
    {
        private readonly ImageStore theImageStore;

        public ImagesController(ImageStore imageStore)
        {
            theImageStore = imageStore;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile image)
        {
            if (image != null)
            {
                using (var stream = image.OpenReadStream())
                {
                    var imageId = await theImageStore.SaveImage(stream);
                    return RedirectToAction("show", new { imageId });
                }

            }
            return View();
        }

        [HttpGet("{imageId}")]
        public ActionResult Show(string imageId)
        {
            var model = new ShowModel { Uri = theImageStore.UriFor(imageId) };
            return View(model);
        }

    }
}