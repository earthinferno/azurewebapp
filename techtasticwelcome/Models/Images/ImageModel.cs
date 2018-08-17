using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace techtasticwelcome.Models.Images
{
    public class ImageModel
    {
        public string ImageUri { get; set; }
        //public ICollection<string> ImagesAnalysis { get; set; }
        public string ImageName { get; set; }
        public string ImageAnalysis { get; set; }
    }
}
