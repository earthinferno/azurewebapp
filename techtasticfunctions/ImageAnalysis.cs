using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Microsoft.WindowsAzure.Storage.Blob;

namespace techtasticfunctions
{
    public static class ImageAnalysis
    {
        [FunctionName("ImageAnalysis")]
        public static void Run([BlobTrigger("images/{name}", Connection = "techtasticstorage")]CloudBlockBlob myBlob, string name, TraceWriter log)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{myBlob.Name} \n Size: {myBlob.Properties.Length} Bytes");

            var sas = GetSas(myBlob);
            var url = myBlob.Uri + sas;

            log.Info($"Blob URL is {url}");
        }

        //public static async Task<Face[]> GetAnalysisAsync(string url)
        //{
        //    var clinet = new FaceServiceClient("1672b8d7de0a41ddab3d9139a956dc76", "https://westeurope.api.cognitive.microsoft.com/face/v1.0");
        //}

        private static string GetSas(CloudBlockBlob blob)
        {
            var sasPolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-15),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15)
            };
            return blob.GetSharedAccessSignature(sasPolicy);
        }

    }
}
