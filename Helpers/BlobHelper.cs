using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;

namespace SalesCodeSpace.Helpers
{
    public class BlobHelper : IBlobHelper
    {
        private readonly BlobServiceClient _blobClient;

        public BlobHelper(IConfiguration configuration)
        {
            string? keys = configuration["Blob:ConnectionString"];
            if (keys?.Contains("${STORAGE_ACCOUNT}") == true)
            {
                string? storageAccount = Environment.GetEnvironmentVariable("STORAGE_ACCOUNT");
                keys = keys.Replace("${STORAGE_ACCOUNT}", storageAccount);
            }

            _blobClient = new BlobServiceClient(keys);
        }

        public async Task DeleteBlobAsync(Guid id, string containerName)
        {
            //BlobContainerClient container = _blobClient.GetBlobContainerClient(containerName);
            var container = _blobClient.GetBlobContainerClient(containerName);
            //var blockBlob = container.GetBlobClient($"{id}");
            var blockBlob = container.GetBlockBlobClient($"{id}");
            await blockBlob.DeleteIfExistsAsync();
        }

        public async Task<Guid> UploadBlobAsync(IFormFile file, string containerName)
        {
            Stream stream = file.OpenReadStream();
            return await UploadBlobAsync(stream, containerName);
        }

        public async Task<Guid> UploadBlobAsync(byte[] file, string containerName)
        {
            MemoryStream stream = new MemoryStream(file);
            return await UploadBlobAsync(stream, containerName);
        }

        public async Task<Guid> UploadBlobAsync(string image, string containerName)
        {
            Stream stream = File.OpenRead(image);
            return await UploadBlobAsync(stream, containerName);
        }

        private async Task<Guid> UploadBlobAsync(Stream stream, string containerName)
        {
            Guid name = Guid.NewGuid();


            var container = _blobClient.GetBlobContainerClient(containerName);
            var blockBlob = container.GetBlockBlobClient($"{name}");
            await blockBlob.UploadAsync(stream);

            return name;
        }
    }

}