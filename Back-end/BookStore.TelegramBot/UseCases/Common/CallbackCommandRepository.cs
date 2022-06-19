using BookStore.TelegramBot.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookStore.TelegramBot.UseCases.Common;
internal class CallbackCommandRepository
{
    private TelegramBotDbContext DbContext { get; }

    public CallbackCommandRepository(
        TelegramBotDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<string> GetAsync(long telegramId, CancellationToken cancellationToken)
    {
        return await DbContext.CallbackCommands
            .Where(command => command.User.TelegramId == telegramId)
            .Select(command => command.CommandLine)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> HasAsync(string commandName, long telegramId, CancellationToken cancellationToken)
    {
        return await DbContext.CallbackCommands
            .Where(command => command.User.TelegramId == telegramId)
            .AnyAsync(command => command.CommandLine.Contains(commandName), cancellationToken);
    }

    public async Task UpdateAsync(string commandLine, long telegramId, CancellationToken cancellationToken)
    {
        var user = await GetUserAsync(telegramId, cancellationToken);

        user.CallbackCommand = new CallbackCommand
        {
            CommandLine = commandLine,
            UserId = user.Id
        };

        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(string commandName, long telegramId, CancellationToken cancellationToken)
    {
        var user = await GetUserAsync(telegramId, cancellationToken);

        if (!user.CallbackCommand.CommandLine.Contains(commandName))
        {
            return;
        }

        var callbackCommand = user.CallbackCommand;
        user.CallbackCommand = null;
        DbContext.CallbackCommands.Remove(callbackCommand);

        await DbContext.SaveChangesAsync(cancellationToken);
    }
    private async Task<TelegramBotUser> GetUserAsync(long telegramId, CancellationToken cancellationToken)
    {
        return await DbContext.TelegramBotUsers
            .Include(user => user.CallbackCommand)
            .SingleAsync(user => user.TelegramId == telegramId, cancellationToken);
    }
}

