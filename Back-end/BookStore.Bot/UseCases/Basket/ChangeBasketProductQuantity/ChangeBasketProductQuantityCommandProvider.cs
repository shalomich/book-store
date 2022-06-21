using BookStore.Bot.Extensions;
using BookStore.Bot.Infrastructure.CommandLineParsers;
using BookStore.Bot.Providers;
using Telegram.Bot.Types;

namespace BookStore.Bot.UseCases.Basket.ChangeBasketProductQuantity;
internal class ChangeBasketProductQuantityCommandProvider
{
    private Update Update { get; }
    public ChangeBasketProductQuantityCommandProvider(
        Update update)
    {
        Update = update;
    }

    public long GetChatId()
    {
        return Update.GetChatId();
    }

    public ChangeBasketProductQuantityCommandArgs GetCommandArgs(string commandLine = null)
    {
        var commandStringArgs = commandLine == null 
            ? Update.TryGetCommand().CommandArgs
            : CommandLineParser.FromCommandLine(commandLine).CommandArgs;

        var commandsArgs = new ChangeBasketProductQuantityCommandArgs
        {
            BasketProductId = int.Parse(commandStringArgs[0]),
            MaxQuantity = int.Parse(commandStringArgs[1]),
            CurrentQuantity = int.Parse(commandStringArgs[2]),
        };

        return commandsArgs;
    }

    public int GetChoosenQuantity()
    {
        var commandArgs = Update.Message.Text.Split(' ');

        return int.Parse(commandArgs[0]);
    }

    public string GetCommandLine()
    {
        var commandArgs = GetCommandArgs();

        var commandArgsAsArray = new object[]
        {
            commandArgs.BasketProductId, commandArgs.MaxQuantity, commandArgs.CurrentQuantity
        };

        return CommandLineParser.ToCommandLine(CommandNames.ChangeBasketProductQuantity, commandArgsAsArray);
    }
}

