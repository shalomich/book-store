using BookStore.TelegramBot.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookStore.TelegramBot;
internal class TelegramBotDbContext : DbContext
{
    public DbSet<TelegramBotUser> TelegramBotUsers { get; set; }
    public DbSet<CallbackCommand> CallbackCommands { get; set; }
    public TelegramBotDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }
}

