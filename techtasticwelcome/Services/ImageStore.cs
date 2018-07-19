using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace techtasticwelcome.Services
{
    public class ImageStore
    {
        CloudBlobClient blobClient;
        string baseUri = "https://techtasticstorage.blob.core.windows.net/";

        public ImageStore()
        {
            var credentials = new StorageCredentials("techtasticstorage", "eDLTv59XKOBX7nPSYmLUQrToWbftjhtL+t+TL5AXGSI/btjLM1pUbnZBdRB3j8FBa3fASLLpngyfMj3ByvVTuw==");
            blobClient = new CloudBlobClient(new Uri(baseUri), credentials);
        }
        public async Task<string> SaveImage(Stream imageStream)
        {
            var imageId = Guid.NewGuid().ToString();
            var blob = getBlob(imageId);
            //var container = blobClient.GetContainerReference("images");
            //var blob = container.GetBlockBlobReference(imageId);
            await blob.UploadFromStreamAsync(imageStream);
            return imageId;
        }

        public CloudBlockBlob getBlob(string imageId)
        {
            var container = blobClient.GetContainerReference("images");
            return container.GetBlockBlobReference(imageId);
        }

        public string UriFor(string imageId)
        {
            var sasPolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-75),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(75)
            };

            var blob = getBlob(imageId);
            //var container = blobClient.GetContainerReference("images");
            //var blob = container.GetBlockBlobReference(imageId);
            var sas = blob.GetSharedAccessSignature(sasPolicy);
            return $"{baseUri}images/{imageId}{sas}";
        }
    }
}
