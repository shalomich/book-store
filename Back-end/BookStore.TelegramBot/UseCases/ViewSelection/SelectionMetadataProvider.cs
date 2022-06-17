using BookStore.Application.Dto;
using BookStore.Application.Providers;
using BookStore.TelegramBot.Extensions;
using BookStore.TelegramBot.UseCases.Common;
using QueryWorker.Args;
using System.Text;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookStore.TelegramBot.UseCases.ViewSelection;
internal class SelectionMetadataProvider
{
    private Update Update { get; }

    public SelectionMetadataProvider(Update update)
    {
        Update = update;
    }

    public string GetSelectionName()
    {
        var command = Update.TryGetCommand().Command;

        return command switch
        {
            CommandNames.Novelties => SelectionNames.Novelties,
            CommandNames.GoneOnSale => SelectionNames.GoneOnSale,
            CommandNames.BackOnSale => SelectionNames.BackOnSale,
            CommandNames.CurrentDayAuthor => SelectionNames.CurrentDayAuthor,
            CommandNames.Popular => SelectionNames.Popular
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

    public long GetChatId()
    {
        return Update.GetChatId();
    }

    public string GetPreviewHtml(TelegramPreviewDto preview)
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

    public InlineKeyboardMarkup BuildNavigationButtons(int totalCount)
    {
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
        else if (pageNumber == maxPageNumber)
        {
            navigationButtons.Remove(navigationButtons.Last());
        }

        return new InlineKeyboardMarkup(navigationButtons);
    }
}