using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage;

namespace UploadFilesToAzureBlob
{
    class ProgramAsync
    {
        static string connectionString = "*";
        static string containerName = "root-container";
        static string folderPath = @"D:\repos\temp\download";
        //static async Task Main(string[] args)
        //{
        //    BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
        //    const string storageAccountName = "*";
        //    const string storageAccountKey = "*";
        //    var credential = new StorageSharedKeyCredential(storageAccountName, storageAccountKey);

        //    //using (var file = File.Open(@"D:\repos\temp\download\1.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
        //    //{
        //    //    BlobClient blobClient1 = new BlobClient(new Uri("*"), credential);
        //    //    await blobClient1.DownloadToAsync(file);
        //    //}

        //    await foreach (var blob in containerClient.GetBlobsAsync())
        //    {
        //        string fileName = blob.Name;

        //        try
        //        {
        //            string localFilePath = Path.Combine(folderPath, fileName);
        //            using (var file = File.Open(localFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
        //            {
        //                var blobClient = containerClient.GetBlobClient(blob.Name);
        //                await blobClient.DownloadToAsync(file);
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }

        //        Console.WriteLine(fileName);
        //    }
        //}

        //public static async Task DataTableToAzure(string tableName)
        //{
        //    BlobServiceClient BlobServiceClient = new BlobServiceClient(connectionString);
        //    var containerClient = BlobServiceClient.GetBlobContainerClient(containerName);
        //    var blobClient = containerClient.GetBlobClient(tableName + ".csv");
        //    //blobClient.

        //}

     
    }
}
