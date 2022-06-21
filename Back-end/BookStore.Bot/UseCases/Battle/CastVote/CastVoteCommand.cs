using AutoMapper;
using BookStore.Application.Queries.UserProfile.GetUserProfile;
using BookStore.Bot.Exceptions;
using BookStore.Bot.Infrastructure;
using BookStore.Bot.Providers;
using BookStore.Bot.UseCases.Common;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookStore.Bot.UseCases.Battle.CastVote;

internal record CastVoteCommand(Update Update) : TelegramBotCommand(Update);

internal class CastVoteCommandHandler : TelegramBotCommandHandler<CastVoteCommand>
{
    private AuthorizedRestClient RestClient { get; }
    private ITelegramBotClient BotClient { get; }
    private IMapper Mapper { get; }
    private UserProfileRestClient UserProfileRestClient { get; }
    private BackEndSettings Settings { get; }
    public CastVoteCommandHandler(
        AuthorizedRestClient restClient,
        IOptions<BackEndSettings> settingsOption,
        ITelegramBotClient botClient,
        IMapper mapper,
        UserProfileRestClient userProfileRestClient)
    {
        RestClient = restClient;
        BotClient = botClient;
        Mapper = mapper;
        UserProfileRestClient = userProfileRestClient;
        Settings = settingsOption.Value;
    }


    protected override async Task Handle(CastVoteCommand request, CancellationToken cancellationToken)
    {
        var prodiver = new CastVoteCommandProvider(request.Update);

        var (userBattleInfo, successToGetProfile) = await TryGetUserBattleInfoAsync(prodiver, cancellationToken);

        if (!successToGetProfile)
        {
            return;
        }

        var successToCastVote = await TryCastVoteAsync(userBattleInfo, prodiver, cancellationToken);

        if (!successToCastVote)
        {
            return;
        }

        await ChooseVotingPointCountAsync(userBattleInfo, prodiver, cancellationToken);
    }

    private async Task<(UserBattleInfoViewModel UserBattleInfo, bool SuccessToGetProfile)> TryGetUserBattleInfoAsync(CastVoteCommandProvider provider, 
        CancellationToken cancellationToken)
    {
        var chatId = provider.GetChatId();

        UserProfileDto profile = null;
        UserBattleInfoViewModel userBattleInfo = null;

        try
        {
            profile = await UserProfileRestClient.GetUserProfileAsync(telegramId: chatId, cancellationToken);
        }
        catch (UnauthorizedException exception)
        {
            await BotClient.SendTextMessageAsync(chatId, exception.Message, cancellationToken: cancellationToken);
            
            return (userBattleInfo, false);
        }

        userBattleInfo = Mapper.Map<UserBattleInfoViewModel>(profile);

        return (userBattleInfo, true);
    }

    private async Task<bool> TryCastVoteAsync(UserBattleInfoViewModel userBattleInfo, CastVoteCommandProvider provider, 
        CancellationToken cancellationToken)
    {
        var chatId = provider.GetChatId();
        var battleBookToVoteId = provider.GetBattleBookId();

        if (userBattleInfo.VotingPointCount == 0)
        {
            await BotClient.SendTextMessageAsync(
                chatId: chatId,
                text: "У вас нет очков для голосования.");

            return false;
        }

        if (userBattleInfo.CurrentVotedBattleBookId.HasValue)
        {
            if (userBattleInfo.CurrentVotedBattleBookId != battleBookToVoteId)
            {
                await BotClient.SendTextMessageAsync(
                   chatId: chatId,
                   text: "В баттле можно голосовать только за одну книгу.");

                return false;
            }
        }
        else
        {
            try
            {
                await RestClient.SendRequestAsync(
                    telegramId: chatId,
                    requestFunction: (client, cancellationToken) => 
                        client.PostAsync(new RestRequest(Settings.CastVotePath)
                            .AddBody(battleBookToVoteId), cancellationToken),
                cancellationToken: cancellationToken);
            }
            catch (UnauthorizedException exception)
            {
                await BotClient.SendTextMessageAsync(chatId, exception.Message, cancellationToken: cancellationToken);

                return false;
            }
        }

        return true;
    }

    private async Task ChooseVotingPointCountAsync(UserBattleInfoViewModel userBattleInfo, CastVoteCommandProvider provider, 
        CancellationToken cancellationToken)
    {
        var votingPointChoiceButtons = provider.GetVotingPointChoiceButtons(userBattleInfo.VotingPointCount);

        await BotClient.SendTextMessageAsync(
            chatId: provider.GetChatId(),
            text: "Сколько будете тратить очков?",
            replyMarkup: votingPointChoiceButtons,
            cancellationToken: cancellationToken);
    }
}

