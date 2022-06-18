using BookStore.Application.Commands.Account.Common;
using BookStore.TelegramBot.Domain;
using BookStore.TelegramBot.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace BookStore.TelegramBot.UseCases.Common;
internal class AuthorizedRestClient
{
    private const string NeedAuthenticateMessage = "Вам необходимо авторизоваться.";
    private RestClient RestClient { get; }
    private TelegramBotDbContext DbContext { get; }
    private ILogger<AuthorizedRestClient> Logger { get; }
    private BackEndSettings Settings { get; }

    public AuthorizedRestClient(
        RestClient restClient,
        TelegramBotDbContext dbContext,
        ILogger<AuthorizedRestClient> logger,
        IOptions<BackEndSettings> settingsOption
    )
    {
        RestClient = restClient;
        DbContext = dbContext;
        Logger = logger;

        Settings = settingsOption.Value;
    }

    public async Task SendRequestAsync(long telegramId, Func<RestClient, CancellationToken, Task<RestResponse>> requestFunction, 
        CancellationToken cancellationToken)
    {
        await SendRequest(telegramId, requestFunction, cancellationToken);   
    }

    public async Task<TResponce> SendRequestAsync<TResponce>(long telegramId, Func<RestClient, CancellationToken, Task<RestResponse>> requestFunction,
        CancellationToken cancellationToken)
    {
        var responce = await SendRequest(telegramId, requestFunction, cancellationToken);

        return JsonConvert.DeserializeObject<TResponce>(responce.Content);
    }

    private async Task<RestResponse> SendRequest(long telegramId, Func<RestClient, CancellationToken, Task<RestResponse>> requestFunction,
        CancellationToken cancellationToken)
    {
        var user = await GetUserAsync(telegramId, cancellationToken);

        var validationTokensResult = ValidateTokens(user);

        if (validationTokensResult == TokenValidationResult.RefreshTokenExpired)
        {
            Logger.LogError("Fail with refresh token expiration.");
            throw new InvalidOperationException(NeedAuthenticateMessage);
        }

        string accessToken = null;


        if (validationTokensResult == TokenValidationResult.AccessTokenValid)
        {
            accessToken = user.AccessToken;
        }
        else if (validationTokensResult == TokenValidationResult.AccessTokenExpired)
        {
            accessToken = await RefreshTokensAsync(user, cancellationToken);
        }

        RestClient.Authenticator = new JwtAuthenticator(accessToken);

        return await requestFunction(RestClient, cancellationToken);
    }

    private async Task<TelegramBotUser> GetUserAsync(long telegramId, CancellationToken cancellationToken)
    {
        var user = await DbContext.TelegramBotUsers
            .SingleOrDefaultAsync(user => user.TelegramId == telegramId, cancellationToken);

        if (user == null)
        {
            Logger.LogError("Fail to get user from database (never login).");
            throw new InvalidOperationException(NeedAuthenticateMessage);
        }

        return user;
    }

    private TokenValidationResult ValidateTokens(TelegramBotUser user)
    {
        var now = DateTimeOffset.UtcNow;

        if (user.AccessTokenExpiration > now)
        {
            return TokenValidationResult.AccessTokenValid;
        }
        else
        {
            return user.RefreshTokenExpiration > now
                ? TokenValidationResult.AccessTokenExpired
                : TokenValidationResult.RefreshTokenExpired;
        }
    }

    private async Task<string> RefreshTokensAsync(TelegramBotUser user, CancellationToken cancellationToken)
    {
        var tokens = new TokensDto
        {
            AccessToken = user.AccessToken,
            RefreshToken = user.RefreshToken
        };

        TokensDto newTokens;

        try
        {
            newTokens = await RestClient.PostAsync<TokensDto>(new RestRequest(Settings.RefreshTokenPath)
                .AddBody(tokens), cancellationToken);
        }
        catch(Exception exception)
        {
            Logger.LogError(exception, "Fail to refresh token");

            throw new InvalidOperationException(NeedAuthenticateMessage);
        }

        user.AccessToken = tokens.AccessToken;
        user.RefreshToken = newTokens.RefreshToken;

        await DbContext.SaveChangesAsync(cancellationToken);

        return user.AccessToken;
    }

    private enum TokenValidationResult
    {
        AccessTokenValid,
        AccessTokenExpired,
        RefreshTokenExpired
    }
}


