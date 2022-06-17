using MediatR;

namespace BookStore.TelegramBot.UseCases.Common;
internal abstract class TelegramBotCommandHandler<T> : AsyncRequestHandler<T> where T : TelegramBotCommand
{
}

