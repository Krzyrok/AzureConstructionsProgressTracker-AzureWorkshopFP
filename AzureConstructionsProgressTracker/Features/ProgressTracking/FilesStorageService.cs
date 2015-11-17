using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureConstructionsProgressTracker.Features.ProgressTracking
{
    public class FilesStorageService
    {
        private const string ContainerName = "constructionprogressfiles";
        
        public async Task<string> UploadFile(string fileName, HttpPostedFileBase file)
        {
            // TODO: 
            // https://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-blobs/#programmatically-access-blob-storage
            // - Add proper nuget
            // - Connect to storage
            // - create container
            // - upload blob
            // - return URL
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=krzyrokazuretraining;AccountKey=WWHibxIfKtbQCQFMoMNVcouAPwNSTpcFF3b77PnyXQ7wVlf9DkGDRFfgul4qUxUKLLkPQrfnHw3r4sWgZWyNBg==");
            
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("krzyrokazuretrainingblobcontainer");

            // Create the container if it doesn't already exist.
            if (container.CreateIfNotExists())
            {
                container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName + Guid.NewGuid());
            blockBlob.UploadFromStream(file.InputStream);

            return blockBlob.Uri.AbsoluteUri;
        }
    }
}