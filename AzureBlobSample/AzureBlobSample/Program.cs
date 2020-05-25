using System;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;
using System.Threading.Tasks;

namespace AzureBlobSample
{
    class Program
    {
        static async Task Main()
        {  
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=rmgssp;AccountKey=MW7QNV86qPvfDEe3Ed03AYank5P5jL0+Ga9BwMVfARjyZOCiLSnc2bfSaziviqAY8dMNAKZ5tgIdEsdkB7+gxA==;EndpointSuffix=core.windows.net";
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            //----code for create container
            //string containerName = "myimage1";
            //BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("myimage1");


            ////----upload file to container
            string fileName = @"D:\MyProject\Images\img2.png";
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileName);

            //----download blob's from container
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                Console.WriteLine("\t" + blobItem.Name);
            }


            string downloadFilePath = @"D:\MyProject\Images\sapna.png";
           
            BlobDownloadInfo download = await blobClient.DownloadAsync();

            using (FileStream downloadFileStream = File.OpenWrite(downloadFilePath))
            {
                await download.Content.CopyToAsync(downloadFileStream);
                downloadFileStream.Close();
            }

            Console.ReadLine();


        }
    }
}
