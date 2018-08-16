using System;
using System.IO;
using System.Threading.Tasks;
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
        public static async Task Run([BlobTrigger("images/{name}", Connection = "techtasticstorage")]
        CloudBlockBlob myBlob, string name, TraceWriter log,
        [CosmosDB("welcometotechtastic", "ttimagesanalysis", ConnectionStringSetting = "ttdb")]
        IAsyncCollector<FaceAnalysisResults> result )
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{myBlob.Name} \n Size: {myBlob.Properties.Length} Bytes");

            var sas = GetSas(myBlob);
            var url = myBlob.Uri + sas;

            log.Info($"Blob URL is {url}");

            var faces = await GetAnalysisAsync(url);
            await result.AddAsync(new FaceAnalysisResults { Faces = faces, ImageId = myBlob.Name });
        }

        public static async Task<Face[]> GetAnalysisAsync(string url)
        {
            var client = new FaceServiceClient("1672b8d7de0a41ddab3d9139a956dc76", "https://westeurope.api.cognitive.microsoft.com/face/v1.0");
            var types = new[] { FaceAttributeType.Emotion };
            var result = await client.DetectAsync(url, false, false, types);
            return result;
        }

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
