using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace techtasticwelcome.Services
{
    public class ImageAnalysisStore
    {
        private DocumentClient _client;
        private Uri _imageAnalysisLink;

        public ImageAnalysisStore()
        {
            var uri = new Uri("https://techtasticdb.documents.azure.com:443/");
            var key = "cqQVpHAXHJmEB3hbbtSAlCvSRVLESYPIZY4gd2bOCLjytTbSne0TFZljYm5WhRoia7yXlF1FKOWmTSFuVAJjYg==";
            _client = new DocumentClient(uri, key);
            _imageAnalysisLink = UriFactory.CreateDocumentCollectionUri("welcometotechtastic", "ttimagesanalysis");
        }

        public dynamic GetImageAnalysis(string imageId)
        {
            var spec = new SqlQuerySpec();
            spec.QueryText = "SELECT c.faces FROM c WHERE (c.ImageId = @imageId)";
            spec.Parameters.Add(new SqlParameter("@imageId", imageId));
            var result = _client.CreateDocumentQuery(_imageAnalysisLink, spec).ToList();//.Last();
            return result;
        }
    }
}
