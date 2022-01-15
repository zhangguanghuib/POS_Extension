using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Azure.Storage.Sas;
using Azure.Storage;

namespace BlobQuickstartV12
{
    class Program
    {
        // https://docs.microsoft.com/en-us/azure/storage/blobs/storage-secure-access-application?tabs=azure-powershell
        const string connectionString = "*";
        const string containerName = "roote-container-20220115";
        const string accountName = "*";
        const string accountKey = "*";
        
        //static async Task Main(string[] args)
        //{
        //    //BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

        //    //BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
        //    //Console.WriteLine("Container " + containerName + " is created");
        //    //await uploadToAzure(containerClient);
        //    //await ListBlobs();
        //    //await DownloadBlobs();

        //    //List<string> fileUrls = await GetThumbNailUrls();

        //   string blobLink = await Utils.uploadQueryResultToAzureCSV("Student");
        //    Console.WriteLine(blobLink);
        //}

        public static async Task<BlobContainerClient> createContainer()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
            return await Task.FromResult(containerClient);
        }

        public static async Task uploadToAzure(BlobContainerClient containerClient)
        {
            string localPath = "./data/";
            string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
            string localFilePath = Path.Combine(localPath, fileName);

            await File.WriteAllTextAsync(localFilePath, "Hello, World");

            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            Console.WriteLine("Uploading to Blob storage as blob:\n\t{0}\n", blobClient.Uri);
            await blobClient.UploadAsync(localFilePath, true);

        }

        public static async Task ListBlobs()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            Console.WriteLine("Listing blobs....");

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                Console.WriteLine("\t" + blobItem.Name);
            }
        }

        public static async Task DownloadBlobs()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            string localPath = "./data/";

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                BlobClient blobClient = containerClient.GetBlobClient(blobItem.Name);
                string localFilePath = Path.Combine(localPath, blobItem.Name);
                string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOADED.txt");
                await blobClient.DownloadToAsync(downloadFilePath);
            }
        }

        public static async Task  DeleteContainer()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.DeleteAsync();
        }

        public static async Task<List<string>> GetThumbNailUrls()
        {
            List<string> thumbNailUrls = new List<string>();
            Uri accountUri = new Uri("https://" +  accountName + ".blob.core.windows.net/");


            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient container = blobServiceClient.GetBlobContainerClient(containerName);
            if (container.Exists())
            {
                BlobSasBuilder sas = new BlobSasBuilder
                {
                    Resource = "c",
                    BlobContainerName = containerName,
                    StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5),
                    ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
                };

                sas.SetPermissions(BlobContainerSasPermissions.All);
                StorageSharedKeyCredential storageCredential = new StorageSharedKeyCredential(accountName,
                    accountKey);

                UriBuilder sasUri = new UriBuilder(accountUri);
                sasUri.Query = sas.ToSasQueryParameters(storageCredential).ToString();

                await foreach (BlobItem blobItem in container.GetBlobsAsync())
                {
                    string sasBlobUri = container.Uri + "/" + blobItem.Name + sasUri.Query;
                    thumbNailUrls.Add(sasBlobUri);
                }
            }

            return await Task.FromResult(thumbNailUrls);
        }
    }
}
