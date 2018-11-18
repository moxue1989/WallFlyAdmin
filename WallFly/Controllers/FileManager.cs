using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace WallFly.Controllers
{
    public class FileManager
    {
        private static readonly string StorageName = "mogeneral";
        private static readonly string StorageApiKey = "WfpQSA5fHzhezp0OCjjVnJjNytggrmaBv+BUjIX4+JH6c75kPflcQu0RTIMUTSPeBPmGEyubnaRPDHL8OOLoIw==";
        private static readonly string VideoContainer = "video";
        private static readonly string AudioContainer = "audio";

        public static IEnumerable<IListBlobItem> GetVideoBlobs()
        {
            CloudStorageAccount storageAccount = new CloudStorageAccount(
                new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
                    StorageName, StorageApiKey), true);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(VideoContainer);
            Task<BlobResultSegment> blobs = cloudBlobContainer.ListBlobsSegmentedAsync(new BlobContinuationToken());
            return blobs.Result.Results;
        }

        public static IEnumerable<IListBlobItem> GetAudioBlobs()
        {
            CloudStorageAccount storageAccount = new CloudStorageAccount(
                new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
                    StorageName, StorageApiKey), true);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(AudioContainer);
            Task<BlobResultSegment> blobs = cloudBlobContainer.ListBlobsSegmentedAsync(new BlobContinuationToken());
            return blobs.Result.Results;
        }
    }
}
