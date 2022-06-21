using MediatR;
using Telegram.Bot.Types;

namespace BookStore.Bot.UseCases.Common;
internal abstract record TelegramBotCommand(Update Update) : IRequest;


