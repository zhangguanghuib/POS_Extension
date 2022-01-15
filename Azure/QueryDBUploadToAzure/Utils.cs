using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobQuickstartV12
{
    class Utils
    {
        const string connectionString = "*";
        const string containerName = "*";
        const string accountName = "*";
        const string accountKey = "*";

        static async Task Main(string[] args)
        {
            string blobLink = await Utils.uploadQueryResultToAzureCSV("Student");
            Console.WriteLine(blobLink);
        }

        public static async Task<string> saveDataTableRoCSV(DataTable dataTable)
        {
            string outputFilePath = Guid.NewGuid().ToString();

            StringBuilder sbCsvContent;
            try
            {
                sbCsvContent = new StringBuilder();
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    sbCsvContent.Append(dataTable.Columns[i].ColumnName);
                    sbCsvContent.Append(i == dataTable.Columns.Count - 1 ? "\r\n" : ",");
                }

                foreach (DataRowCollection row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        sbCsvContent.Append(row[i]);
                        sbCsvContent.Append(i == dataTable.Columns.Count - 1 ? "\r\n" : ",");
                    }
                }

                await File.WriteAllTextAsync(outputFilePath, sbCsvContent.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(outputFilePath);
        }

        public static async Task<string> uploadQueryResultToAzureCSV(string tableName)
        {
            DataTable dataTable = await loadDataAsync(tableName);

            string CSVText = buildCSVText(dataTable);

            string tableNameInAzure = await uploadToAzureAsync(CSVText, tableName);
            string blobDownloadLink = await GetDownloadableUrl(tableNameInAzure);

            return await Task.FromResult(blobDownloadLink);
        }

        public static string buildCSVText(DataTable dataTable)
        {
            StringBuilder sbCsvContent = new StringBuilder();
            try
            {
                sbCsvContent = new StringBuilder();
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    sbCsvContent.Append(dataTable.Columns[i].ColumnName);
                    sbCsvContent.Append(i == dataTable.Columns.Count - 1 ? "\r\n" : ",");
                }

                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        sbCsvContent.Append(row[i]);
                        sbCsvContent.Append(i == dataTable.Columns.Count - 1 ? "\r\n" : ",");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return sbCsvContent.ToString();
        }

        public static async Task<string> uploadToAzureAsync(string csvText, string tableName)
        {
            string fileName = tableName + Guid.NewGuid().ToString()+".csv";

            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);

            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(csvText)))
            {
                Response<BlobContentInfo> rersponse = await containerClient.UploadBlobAsync(fileName, stream);

                Console.WriteLine(fileName + " uploaded!");
            }

            return await Task.FromResult(fileName);
        }


        static async Task<DataTable> loadDataAsync(string tableName)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "(local)";
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "SchoolDB";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * from dbo." + tableName;

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return dataTable;
        }


        public static async Task<string> GetDownloadableUrl(string tableNameInAzure)
        {
            string blobDownloadLink = "";
            Uri accountUri = new Uri("https://" + accountName + ".blob.core.windows.net/");

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
                    if(string.Equals(blobItem.Name, tableNameInAzure))
                    {
                        blobDownloadLink = container.Uri + "/" + blobItem.Name + sasUri.Query;
                    }
                }
            }

            return await Task.FromResult(blobDownloadLink);
        }
    }

}
