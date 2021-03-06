﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using techtasticwelcome.Models.Images;

namespace techtasticwelcome.Helpers
{
    public interface IAzureBlobStorage
    {
        //Task<MemoryStream> DownloadAsync(string blobName);
        Task<List<AzureBlobItem>> ListAsync();
        Task<List<string>> ListFolderAsync();
        Task UploadAsync(string blobName, Stream stream);
        Task<string> BlobUri(string baseUri, string blobName);
        Task<List<string>> GetBlobUris(string baseUri);
        Task<List<ImageModel>> GetImageBlobs(string baseUri);
    }
}