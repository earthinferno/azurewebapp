﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace techtasticwelcome.Models.Images
{
    public class ImagesModel
    {
        //public ICollection<string> ImagesUri { get; set; }
        //public ICollection<string> ImagesAnalysis { get; set; }
        public ICollection<ImageModel> Images { get; set; }
    }
}
