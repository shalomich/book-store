namespace BookStore.Application.Providers;

public class S3Settings
{
    public string AccessKey { get; init; }

    public string SecretKey { get; init; }

    public string ServiceUrl { get; init; }

    public string BucketName { get; init; }

    public string RegionName { get; init; }

    public int PresignedUrlExpireDays { get; init; }
}

