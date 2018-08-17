using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using techtasticwelcome.Helpers;
using techtasticwelcome.Models.Images;

namespace techtasticwelcome.Services
{
    public class ImageStore
    {
        //string baseUri = "https://techtasticstorage.blob.core.windows.net/";
        string _baseUri;
        IConfiguration _configuration;


        private readonly IAzureBlobStorage _blobStorage;

        public ImageStore(IAzureBlobStorage azureBlobStorage, IConfiguration configuration)
        {
            _blobStorage = azureBlobStorage;
            _configuration = configuration;
            _baseUri = _configuration["Blob_BaseUri"];
        }
        public async Task<string> SaveImage(Stream imageStream)
        {
            var imageId = Guid.NewGuid().ToString();
            await _blobStorage.UploadAsync(imageId, imageStream);
            return imageId;
        }

        public string UriFor(string imageId)
        {
            return _blobStorage.BlobUri(_baseUri, imageId).Result;
        }

        public List<string> GetImageUris()
        {
            return _blobStorage.GetBlobUris(_baseUri).Result;
        }

        public List<ImageModel> GetImageBlobs()
        {
            return _blobStorage.GetImageBlobs(_baseUri).Result;
        }

    }
}
