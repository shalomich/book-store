using BookStore.TelegramBot.Extensions;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.UseCases.Battle.SpendVotingPoints;
internal class SpendVotingPointsCommandProvider
{
    private Update Update { get; }
    public bool IsCallback { get; set; } = false;

    public SpendVotingPointsCommandProvider(
        Update update)
    {
        Update = update;
    }

    public int? GetVotingPointCount()
    {
        string[] commandArgs;

        if (IsCallback)
        {
            commandArgs = Update.Message.Text.Split(' ');
        }
        else
        {
            var tryGetCommandResult = Update.TryGetCommand();

            if (!tryGetCommandResult.HasCommandArgs)
            {
                return null;
            }

            commandArgs = tryGetCommandResult.CommandArgs;
        }
        
        int votingPointCount;

        try
        {
            votingPointCount = int.Parse(commandArgs[0]);
        }
        catch (Exception)
        {
            return null;
        }

        return votingPointCount;
    }

    public long GetChatId()
    {
        return Update.GetChatId();
    }
}

