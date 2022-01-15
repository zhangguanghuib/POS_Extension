using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace UploadFilesToAzureBlob
{
    class Program
    {
        static string connectionString = "*";
        static string containerName = "*";
        static string folderPath = @"D:\repos\temp\";
        static async Task Main(string[] args)
        {
            await readAzureFile("Hello2.csv");
            //downloadBlobs();
            //upload();
           // await DataTableToAzure("Hello2");
           // Console.Read();
        }

        public static void upload()
        {
            var files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);

            foreach (var file in files)
            {
                var filePathOverCloud = file.Replace(folderPath, string.Empty);
                using (MemoryStream stream = new MemoryStream(File.ReadAllBytes(file)))
                {
                    Response<BlobContentInfo> rersponse = containerClient.UploadBlob(filePathOverCloud, stream);

                    Console.WriteLine(filePathOverCloud + " uploaded!");
                }
            }

            Console.Read();
        }

        public static async Task<string> DataTableToAzure(string tableName)
        {
            //BlobServiceClient BlobServiceClient = new BlobServiceClient(connectionString);
            //var containerClient = BlobServiceClient.GetBlobContainerClient(containerName);
            //var blobClient = containerClient.GetBlobClient(tableName + ".csv");
            //blobClient.


            //using (var outStream = await blobClient.OpenWriteAsync(true).ConfigureAwait(false))
            //using (ChoParquetWriter parser = new ChoParquetWriter(outStream))
            //{
            //    parser.Write(data_list);
            //}

            string text = "hello world for azure test";
            string blobName = tableName + ".csv";
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(text)))
            {
                Response<BlobContentInfo> rersponse = await containerClient.UploadBlobAsync(blobName, stream);

                BlobClient blobClient = containerClient.GetBlobClient(tableName);
                Uri  uri1 =  blobClient.GenerateSasUri(Azure.Storage.Sas.BlobSasPermissions.All, DateTimeOffset.Now.AddMinutes(10));
                Uri uri = blobClient.Uri;
                Console.WriteLine(blobName + "uploaded");
            }

            return "Succeeded";
        }

        public static async Task<string> readAzureFile(string tableName)
        {
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            var blobClient = containerClient.GetBlobClient(tableName);
            var result = await blobClient.DownloadContentAsync();
            Console.WriteLine(result.Value.Content.ToString());
            return result.Value.Content.ToString();

        }

        public static void downloadBlobs()
        {
            //const string StorageAccountName = "*";
            //const string StorageAccountKey = "*";

            //var storageAccount = new CloudStorageAccount(new StorageCredentials(StorageAccountName, StorageAccountKey), true);

            //var blobClient = storageAccount.CreateCloudBlobClient();
            //var container = blobClient.GetContainerReference(containerName);

            //var blobs = container.

            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            
            //containerClient.get
        }
    }
}
