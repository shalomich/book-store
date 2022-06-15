namespace BookStore.Application.Providers;
public record FrontEndSettings
{
    public string DashboardUrl { get; init; }
    public string StoreUrl { get; init; }
    public string ResetPasswordPath { get; init; }
}

