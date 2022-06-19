using Microsoft.Extensions.Configuration;

namespace BookStore.TelegramBot.UseCases.Common;
internal static class StoreUrlBuilder
{
    public static string BuildBookСardUrl(int bookId, IConfiguration configuration)
    {
        var storeUrl = configuration["FrontEnd:StoreUrl"];
        var bookCardPath = configuration["FrontEnd:BookCardPath"];

        return $"{storeUrl}{bookCardPath}{bookId}";
    }
}

