using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace techtasticwelcome.Helpers
{
    public class AzureBlobItem
    {
        private readonly SharedAccessBlobPolicy _sasPolicy;
        public AzureBlobItem(IListBlobItem item)
        {
            Item = item;
            _sasPolicy = sasPolicy();
            BlockBlobSharedAccessSignature = Item.GetType() == typeof(CloudBlockBlob) ? ((CloudBlockBlob)Item).GetSharedAccessSignature(_sasPolicy) : null;
        }

        public IListBlobItem Item { get; }
        public string BlockBlobSharedAccessSignature { get; }

        public bool IsBlockBlob => Item.GetType() == typeof(CloudBlockBlob);
        public bool IsPageBlob => Item.GetType() == typeof(CloudPageBlob);
        public bool IsDirectory => Item.GetType() == typeof(CloudBlobDirectory);
        public string BlobName => IsBlockBlob ? ((CloudBlockBlob)Item).Name :
            IsPageBlob ? ((CloudPageBlob)Item).Name :
            IsDirectory ? ((CloudBlobDirectory)Item).Prefix : "";
        public string Folder => BlobName.Contains("/") ? BlobName.Substring(BlobName.LastIndexOf("/" + 1)) : BlobName;

        //public string BlockBlobSharedAccessSignature => Item.GetType() == typeof(CloudBlockBlob) ? ((CloudBlockBlob)Item).GetSharedAccessSignature(_sasPolicy) : null;


        private string GetSharedAccessSignature()
        {
            var sasPolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-15),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15)
            };

            return ((CloudBlockBlob)Item).GetSharedAccessSignature(sasPolicy);
        }

        private SharedAccessBlobPolicy sasPolicy()
        {
            return new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-15),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15)
            };
        }
    }
}
