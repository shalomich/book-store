using MediatR;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.UseCases.Common;
internal abstract record TelegramBotCommand(Update Update) : IRequest;


