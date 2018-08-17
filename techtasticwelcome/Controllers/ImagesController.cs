using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using techtasticwelcome.Helpers;
using techtasticwelcome.Models.Images;
using techtasticwelcome.Services;

namespace techtasticwelcome.Controllers
{
    [Route("[controller]/[action]")]
    public class ImagesController : Controller
    {
        private readonly ImageStore _imageStore;
        private readonly ImageAnalysisStore _imageAnalysisStore;

        public ImagesController(ImageStore imageStore, ImageAnalysisStore imageAnalysisStore)
        {
            _imageStore = imageStore;
            _imageAnalysisStore = imageAnalysisStore;
        }

        public IActionResult Index()
        {
            //var images = _imageStore.GetImageBlobs();
            //var model = new ImagesModel { ImagesUri = _imageStore.GetImageUris() };
            var model = new ImagesModel { Images = _imageStore.GetImageBlobs() };
            foreach(var image in model.Images)
            {
                if (!string.IsNullOrEmpty(image.ImageName))
                {
                    var analysis = _imageAnalysisStore.GetImageAnalysis(image.ImageName);
                    if (analysis != null)
                    {
                        image.ImageAnalysis = JsonConvert.SerializeObject(analysis,
                                                            Formatting.Indented);
                    }

                    //image.ImageAnalysis = _imageAnalysisStore.GetImageAnalysis(image.ImageName).ToString();
                }
            }
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