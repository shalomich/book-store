using BookStore.Application.Exceptions;
using BookStore.Domain.Entities.Products;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Editing.UpdateDiscount;

public record SetDiscountCommand(int ProductId, DiscountForm DiscountForm) : IRequest;
internal class SetDiscountHandler : AsyncRequestHandler<SetDiscountCommand>
{
    private ApplicationContext Context { get;}

    public SetDiscountHandler(ApplicationContext context)
    {
        Context = context;
    }

    protected override async Task Handle(SetDiscountCommand request, CancellationToken cancellationToken)
    {
        await Valdiate(request);

        var (productId, discountForm) = request;

        var discountByProductId = await Context.Discounts
            .SingleOrDefaultAsync(discount => discount.ProductId == productId);
        
        if (discountByProductId != null)
        {
            Context.Discounts.Remove(discountByProductId);
        }

        var newDiscount = new Discount()
        {
            ProductId = productId,
            Percentage = discountForm.Percentage,
            EndDate = discountForm.EndDate
        };

        Context.Discounts.Add(newDiscount);

        await Context.SaveChangesAsync(cancellationToken);
    }

    private async Task Valdiate(SetDiscountCommand request)
    {
        var product = await Context.FindAsync(typeof(Product), request.ProductId);

        if (product == null)
        {
            throw new NotFoundException("There is no product with this id");
        }

        var discountForm = request.DiscountForm;

        if (discountForm.EndDate < DateTimeOffset.Now)
        {
            throw new BadRequestException("End date can't be early than current day.");
        }
    }
}

