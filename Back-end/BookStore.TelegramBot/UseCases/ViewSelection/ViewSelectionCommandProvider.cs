using BookStore.Application.Dto;
using BookStore.Application.Providers;
using BookStore.TelegramBot.Extensions;
using BookStore.TelegramBot.UseCases.Common;
using QueryWorker.Args;
using System.Text;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookStore.TelegramBot.UseCases.ViewSelection;
internal class ViewSelectionCommandProvider
{
    private Update Update { get; }

    public ViewSelectionCommandProvider(Update update)
    {
        Update = update;
    }
    public long GetChatId()
    {
        return Update.GetChatId();
    }

    public (string SelectionName, bool NeedToAuthorize) GetSelectionName()
    {
        var command = Update.TryGetCommand().Command;

        return command switch
        {
            CommandNames.Novelties => (SelectionNames.Novelties, false),
            CommandNames.GoneOnSale => (SelectionNames.GoneOnSale, false),
            CommandNames.BackOnSale => (SelectionNames.BackOnSale, false),
            CommandNames.CurrentDayAuthor => (SelectionNames.CurrentDayAuthor, false),
            CommandNames.Popular => (SelectionNames.Popular, false),
            CommandNames.LastViewed => (SelectionNames.LastViewed, true),
            CommandNames.CanBeInteresting => (SelectionNames.CanBeInteresting, true),
            CommandNames.SpecialForYou => (SelectionNames.SpecialForYou, true)
        };
    }

    public OptionParameters BuildPagging()
    {
        int pageNumber;

        var tryGetCommandResult = Update.TryGetCommand();

        if (tryGetCommandResult.HasCommandArgs)
        {
            pageNumber = int.Parse(tryGetCommandResult.CommandArgs[0]);
        }
        else
        {
            pageNumber = 1;
        }

        return new OptionParameters
        {
            Pagging = new PaggingArgs
            {
                PageNumber = pageNumber,
                PageSize = 2
            }
        };
    }

    public string GetPreviewHtml(PreviewViewModel preview)
    {
        var builder = new StringBuilder();

        builder.Append($"<b>Название: </b>" +
            $"<a href=\"{preview.StoreUrl}\"><i>{preview.Name}</i></a>\n");
        builder.Append($"<b>Автор: </b><i>{preview.AuthorName}</i>\n");
        builder.Append($"<b>Издатель: </b><i>{preview.PublisherName}</i>\n");
        builder.Append($"<b>Цена: </b> {preview.Cost}\n");

        if (preview.DiscountCost.HasValue)
        {
            builder.Append($"<u><b>Цена со скидкой: </b>{preview.DiscountCost}</u>\n");
        }

        return builder.ToString();
    }

    public bool TryGetNavigationButtons(int totalCount, out InlineKeyboardMarkup keyboard)
    {
        keyboard = null;
        var pagging = BuildPagging().Pagging;
        var pageNumber = pagging.PageNumber;
        var maxPageNumber = (int)Math.Ceiling(totalCount / (double)pagging.PageSize);

        var command = Update.TryGetCommand().Command;

        int previousPage = pageNumber - 1;
        int nextPage = pageNumber + 1;

        var navigationButtons = new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithCallbackData(text: "Предыдущая страница", callbackData: $"/{command} {previousPage}"),
            InlineKeyboardButton.WithCallbackData(text: "Следующая страница", callbackData: $"/{command} {nextPage}")
        };

        if (pageNumber == 1)
        {
            navigationButtons.Remove(navigationButtons.First());
        }
        
        if (pageNumber == maxPageNumber)
        {
            navigationButtons.Remove(navigationButtons.Last());
        }

        if (!navigationButtons.Any())
        {
            return false;
        }

        keyboard = new InlineKeyboardMarkup(navigationButtons);

        return true;
    }
}