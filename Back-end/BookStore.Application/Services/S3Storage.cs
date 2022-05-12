namespace BookStore.Application.Services;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using BookStore.Application.Providers;
using Microsoft.Extensions.Options;

public class S3Storage : IDisposable
{
    private readonly AmazonS3Client s3Client;
    private readonly S3Settings settings;
    
    public S3Storage(IOptions<S3Settings> settingOption)
    {
        settings = settingOption.Value;
        
        var credentials = new BasicAWSCredentials(settings.AccessKey, settings.SecretKey);
        var config = new AmazonS3Config
        {
            RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(settings.RegionName),
            ServiceURL = settings.ServiceUrl,
            ForcePathStyle = true
        };

        s3Client = new AmazonS3Client(credentials, config);
    }

    public string GetPresignedUrlForViewing(int productId, int imageId)
    {
        DateTimeOffset expireDate = DateTimeOffset.UtcNow.AddDays(settings.PresignedUrlExpireDays);

        var presignedUrl = s3Client.GetPreSignedURL(new GetPreSignedUrlRequest
        {
            BucketName = settings.BucketName,
            Key = $"{productId}/{imageId}",
            Expires = expireDate.DateTime,
            Protocol = GetServiceUrlProtocol(),
            Verb = HttpVerb.GET
        });

        return presignedUrl;
    }

    public async Task PostAsync(string path, Stream fileStream, CancellationToken cancellationToken = default)
    {
        using var transferUtility = new TransferUtility(s3Client);
        
        var uploadRequest = new TransferUtilityUploadRequest()
        {
            CannedACL = S3CannedACL.Private,
            InputStream = fileStream,
            BucketName = settings.BucketName,
            Key = path,
        };
        await transferUtility.UploadAsync(uploadRequest, cancellationToken);
    }

    public async Task RemoveAsync(string path, CancellationToken cancellationToken = default)
    {
        await s3Client.DeleteObjectAsync(settings.BucketName, path, cancellationToken);
    }

    private Protocol GetServiceUrlProtocol()
    {
        if (Uri.TryCreate(settings.ServiceUrl, UriKind.Absolute, out var serviceUri))
        {
            return serviceUri.Scheme == "http" ? Protocol.HTTP : Protocol.HTTPS;
        }

        return Protocol.HTTPS;
    }

    #region IDisposable

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            s3Client.Dispose();
        }
    }

    #endregion IDisposable
}
