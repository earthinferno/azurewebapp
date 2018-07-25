using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using techtasticwelcome.Helpers;
using techtasticwelcome.Models.Images;
using techtasticwelcome.Services;

namespace techtasticwelcome.Controllers
{
    [Route("[controller]/[action]")]
    public class ImagesController : Controller
    {
        private readonly ImageStore _imageStore;

        public ImagesController(ImageStore imageStore)
        {
            _imageStore = imageStore;
        }

        public IActionResult Index()
        {
            var model = new ImagesModel { ImagesUri = _imageStore.GetImageUris() };
            return View(model);
        }

        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile image)
        {
            if (image != null)
            {
                using (var stream = image.OpenReadStream())
                {
                    var imageId = await _imageStore.SaveImage(stream);
                    return RedirectToAction("show", new { imageId });
                }

            }
            return View();
        }


        [HttpGet("{imageId}")]
        public ActionResult Show(string imageId)
        {
            var model = new ShowModel { Uri = _imageStore.UriFor(imageId) };
            return View(model);
        }

    }
}