using BookStore.Application.Queries.UserProfile.GetUserProfile;
using BookStore.Bot.Infrastructure;
using BookStore.Bot.Providers;
using Microsoft.Extensions.Options;
using RestSharp;

namespace BookStore.Bot.UseCases.Common;
internal class UserProfileRestClient
{
    private AuthorizedRestClient RestClient { get; }
    private BackEndSettings Settings { get; }

    public UserProfileRestClient(
        AuthorizedRestClient restClient,
        IOptions<BackEndSettings> settingsOption
    )
    {
        RestClient = restClient;

        Settings = settingsOption.Value;

    }

    public async Task<UserProfileDto> GetUserProfileAsync(long telegramId, CancellationToken cancellationToken)
    {
        return await RestClient.SendRequestAsync<UserProfileDto>(
            telegramId: telegramId,
            requestFunction: (client, cancellationToken)
                => client.GetAsync(new RestRequest(Settings.UserProfilePath), cancellationToken),
            cancellationToken: cancellationToken);
    }
}

