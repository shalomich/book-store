using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.TelegramBot.Notifications.NotifyBackOnSale;
internal record NotifyBackOnSaleMessageData
{
    public string BookName { get; init; }
    public string AuthorName { get; init; }
}

