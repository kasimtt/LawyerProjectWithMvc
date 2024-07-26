using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using LawyerProject.Application.Abstractions.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage :BaseStorage, IAzureStorage   //kaynak : gencay yıldız ders 31...
    {
        private readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;

        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration["Storage:Azure"]);
        }
        public async Task DeleteAsync(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string containerName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Select(f=>f.Name).ToList();
        }

        public bool HasFile(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Any(f => f.Name == fileName);
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);  //Public access is not permitted on this storage account. hatası aldım da bakacaz artık 

            List<(string fileName, string pathOrContainerName)> datas = new();
            foreach (var file in files) 
            {
                
                string NewFileName = await FileRenameAsync(containerName, file.FileName,HasFile);
                BlobClient blobClient = _blobContainerClient.GetBlobClient(NewFileName);
                await blobClient.UploadAsync(file.OpenReadStream());
                datas.Add((NewFileName, $"{containerName}/{NewFileName}"));
            
            }

            return datas;
        }
    }
}
