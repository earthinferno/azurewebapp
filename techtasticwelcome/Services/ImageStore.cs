using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using techtasticwelcome.Helpers;

namespace techtasticwelcome.Services
{
    public class ImageStore
    {
        CloudBlobClient _blobClient;
        string baseUri = "https://techtasticstorage.blob.core.windows.net/";

        private readonly IAzureBlobStorage _blobStorage;

        public ImageStore(IAzureBlobStorage azureBlobStorage)
        {
            var credentials = new StorageCredentials("techtasticstorage", "eDLTv59XKOBX7nPSYmLUQrToWbftjhtL+t+TL5AXGSI/btjLM1pUbnZBdRB3j8FBa3fASLLpngyfMj3ByvVTuw==");
            _blobClient = new CloudBlobClient(new Uri(baseUri), credentials);

            _blobStorage = azureBlobStorage;
        }
        public async Task<string> SaveImage(Stream imageStream)
        {
            //var imageId = Guid.NewGuid().ToString();
            //var blob = getBlob(imageId);
            //await blob.UploadFromStreamAsync(imageStream);
            //return imageId;
            var imageId = Guid.NewGuid().ToString();
            await _blobStorage.UploadAsync(imageId, imageStream);
            return imageId;
        }

        //public CloudBlockBlob getBlob(string imageId)
        //{
        //    //var container = _blobClient.GetContainerReference("images");
        //    //return container.GetBlockBlobReference(imageId);
        //    return _blobStorage.(imageId);
        //}

        public string UriFor(string imageId)
        {
            return _blobStorage.BlobUri(baseUri, imageId).Result;
            //var sasPolicy = new SharedAccessBlobPolicy
            //{
            //    Permissions = SharedAccessBlobPermissions.Read,
            //    SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-15),
            //    SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15)
            //};

            //var blob = _blobStorage.DownloadAsync(imageId);
            //var blob = getBlob(imageId);
            //var sas = blob.GetSharedAccessSignature(sasPolicy);
            //return $"{baseUri}images/{imageId}{sas}";
        }

        public List<string> GetImageUris()
        {
            // return GetBlobUrisAsync(blobContainerName).Result;
            return _blobStorage.GetBlobUris(baseUri).Result;
        }

        //public async Task<ICollection<string>> GetBlobUrisAsync(string blobContainerName)
        //{
        //    BlobContinuationToken continuationToken = null;
        //    var sasPolicy = GetSasPolicy();
        //    var container = _blobClient.GetContainerReference(blobContainerName);
        //    var sas = container.GetSharedAccessSignature(sasPolicy);

        //    var results = new List<IListBlobItem>();
        //    var results = new List<string>();
        //    do
        //    {
        //        var response = await container.ListBlobsSegmentedAsync(continuationToken);
        //        continuationToken = response.ContinuationToken;
        //        foreach (IListBlobItem blob in response.Results)
        //        {
        //            results.Add($"{baseUri}{blob.Uri}{sas}");
        //        }
        //    }
        //    while (continuationToken != null);
        //    return results;
        //}

        //private SharedAccessBlobPolicy GetSasPolicy()
        //{
        //    return new SharedAccessBlobPolicy
        //    {
        //        Permissions = SharedAccessBlobPermissions.Read,
        //        SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-15),
        //        SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15)
        //    };
        //}
    }
}
