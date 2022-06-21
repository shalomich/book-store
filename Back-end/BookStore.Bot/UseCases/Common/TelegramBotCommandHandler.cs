using MediatR;

namespace BookStore.Bot.UseCases.Common;
internal abstract class TelegramBotCommandHandler<T> : AsyncRequestHandler<T> where T : TelegramBotCommand
{
}

