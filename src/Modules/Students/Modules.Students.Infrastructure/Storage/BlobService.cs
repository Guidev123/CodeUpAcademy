using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Modules.Students.Application.Services;

namespace Modules.Students.Infrastructure.Storage;

internal sealed class BlobService : IBlobService
{
    private readonly BlobServiceClient _blob;
    private readonly string _containerName;

    public BlobService(BlobServiceClient blob, IConfiguration configuration)
    {
        _blob = blob;
        _containerName = configuration.GetSection("BlobStorageConfig:ContainerName").Value ?? string.Empty;
    }

    public async Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        var containerClient = _blob.GetBlobContainerClient(_containerName);
        var blobClient = containerClient.GetBlobClient(fileId.ToString());

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    public async Task<string> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken = default)
    {
        var containerClient = _blob.GetBlobContainerClient(_containerName);

        var fileId = Guid.NewGuid();
        var blobClient = containerClient.GetBlobClient(fileId.ToString());

        await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = contentType }, cancellationToken: cancellationToken);

        return blobClient.Uri.ToString();
    }
}
