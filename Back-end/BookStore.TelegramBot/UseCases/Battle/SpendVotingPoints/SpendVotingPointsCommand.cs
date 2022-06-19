using AutoMapper;
using BookStore.Application.Queries.UserProfile.GetUserProfile;
using BookStore.TelegramBot.Providers;
using BookStore.TelegramBot.UseCases.Battle.CastVote;
using BookStore.TelegramBot.UseCases.Common;
using Microsoft.Extensions.Options;
using RestSharp;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.UseCases.Battle.SpendVotingPoints;

internal record SpendVotingPointsCommand(Update Update) : TelegramBotCommand(Update);

internal class SpendVotingPointsCommandHandler : TelegramBotCommandHandler<SpendVotingPointsCommand>
{
    private AuthorizedRestClient RestClient { get; }
    private ITelegramBotClient BotClient { get; }
    private IMapper Mapper { get; }
    private UserProfileRestClient UserProfileRestClient { get; }
    private CallbackCommandRepository CallbackCommandRepository { get; }
    private BackEndSettings Settings { get; }
    public SpendVotingPointsCommandHandler(
        AuthorizedRestClient restClient,
        IOptions<BackEndSettings> settingsOption,
        ITelegramBotClient botClient,
        IMapper mapper,
        UserProfileRestClient userProfileRestClient,
        CallbackCommandRepository callbackCommandRepository)
    {
        RestClient = restClient;
        BotClient = botClient;
        Mapper = mapper;
        UserProfileRestClient = userProfileRestClient;
        CallbackCommandRepository = callbackCommandRepository;
        Settings = settingsOption.Value;
    }


    protected override async Task Handle(SpendVotingPointsCommand request, CancellationToken cancellationToken)
    {
        var prodiver = new SpendVotingPointsCommandProvider(request.Update);
        var telegramId = prodiver.GetChatId();

        bool isCallback = await CallbackCommandRepository.HasAsync(CommandNames.SpendVotingPoints,
            telegramId, cancellationToken);

        prodiver.IsCallback = isCallback;
        
        var (userBattleInfo, successToGetProfile) = await TryGetUserBattleInfoAsync(prodiver, cancellationToken);

        if (!successToGetProfile)
        {
            return;
        }

        var (votingPointCount, successToGetVotingPoints) = await TryGetVotingPointCountAsync(userBattleInfo, prodiver, cancellationToken);

        if (!successToGetVotingPoints)
        {
            return;
        }

        await SpendVotingPointsAsync(votingPointCount.Value, prodiver, cancellationToken);

        if (isCallback)
        {
            await CallbackCommandRepository.RemoveAsync(CommandNames.SpendVotingPoints, telegramId,
                cancellationToken);
        }
    }

    private async Task<(UserBattleInfoViewModel UserBattleInfo, bool SuccessToGetProfile)> TryGetUserBattleInfoAsync(SpendVotingPointsCommandProvider provider,
        CancellationToken cancellationToken)
    {
        var chatId = provider.GetChatId();

        UserProfileDto profile = null;
        UserBattleInfoViewModel userBattleInfo = null;

        try
        {
            profile = await UserProfileRestClient.GetUserProfileAsync(
                telegramId: chatId, cancellationToken);
        }
        catch (InvalidOperationException exception)
        {
            await BotClient.SendTextMessageAsync(
               chatId: chatId,
               text: exception.Message);

            return (userBattleInfo, false);
        }

        userBattleInfo = Mapper.Map<UserBattleInfoViewModel>(profile);

        return (userBattleInfo, true);
    }

    private async Task<(int? VotingPointCount, bool SuccessToGetVotingPoints)> TryGetVotingPointCountAsync(
        UserBattleInfoViewModel userBattleInfo, SpendVotingPointsCommandProvider provider, CancellationToken cancellationToken)
    {
        var chatId = provider.GetChatId();

        int? votingPointCount = 0;

        if (userBattleInfo.VotingPointCount == 0)
        {
            await BotClient.SendTextMessageAsync(
                chatId: chatId,
                text: "У вас нет очков для голосования.");

            return (votingPointCount, false);
        }

        if (!userBattleInfo.CurrentVotedBattleBookId.HasValue)
        {
            string showBattleCommandLine = CommandLineParser.ToCommandLine(CommandNames.ShowBattle);

            await BotClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Выберите комикс, за который вы хотите голосовать {showBattleCommandLine}.");

            return (votingPointCount, false);
        }

        votingPointCount = provider.GetVotingPointCount();

        if (!votingPointCount.HasValue)
        {
            await CallbackCommandRepository.UpdateAsync(
                commandLine: CommandLineParser.ToCommandLine(CommandNames.SpendVotingPoints), 
                telegramId: chatId, cancellationToken);
            
            await BotClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Введите количество очков, которые вы хотите отдать на голосование (всего у вас {userBattleInfo.VotingPointCount}).");

            return (votingPointCount, false);
        }
    
        if (votingPointCount > userBattleInfo.VotingPointCount)
        {
            await BotClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Недостаточно очков (всего у вас {userBattleInfo.VotingPointCount}).");

            return (votingPointCount, false);
        }

        return (votingPointCount, true);
    }

    private async Task SpendVotingPointsAsync(int votingPointCount, SpendVotingPointsCommandProvider provider,
        CancellationToken cancellationToken)
    {
        var chatId = provider.GetChatId();

        try
        {
            await RestClient.SendRequestAsync(
                telegramId: chatId,
                requestFunction: (client, cancellationToken) =>
                    client.PutAsync(new RestRequest(Settings.SpendVotingPointsPath)
                        .AddBody(votingPointCount)),
                cancellationToken: cancellationToken);
        }
        catch (InvalidOperationException exception)
        {
            await BotClient.SendTextMessageAsync(
               chatId: chatId,
               text: exception.Message);

            return;
        }

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Очки голосования потрачены успешно.");
    }
}

