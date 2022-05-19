using BookStore.Domain.Entities.Products;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.BookEditing.RemoveDiscount;

public record RemoveDiscountCommand() : IRequest;
internal class RemoveDiscountHandler : AsyncRequestHandler<RemoveDiscountCommand>
{
    private ApplicationContext Context { get; }

    public RemoveDiscountHandler(ApplicationContext context)
    {
        Context = context;
    }

    protected override async Task Handle(RemoveDiscountCommand request, CancellationToken cancellationToken)
    {
        var now = DateTimeOffset.Now;

        var expiredDiscounts = await Context.Discounts
            .Where(discount => discount.EndDate < now)
            .ToListAsync(cancellationToken);

        Context.Discounts.RemoveRange(expiredDiscounts);

        await Context.SaveChangesAsync(cancellationToken);
    }
}

