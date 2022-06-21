namespace BookStore.Bot.Providers;
internal static class StoreUrlBuilder
{
    public static string BuildBookСardUrl(int bookId, IConfiguration configuration)
    {
        var storeUrl = configuration["FrontEnd:StoreUrl"];
        var bookCardPath = configuration["FrontEnd:BookCardPath"];

        return $"{storeUrl}{bookCardPath}{bookId}";
    }
}

