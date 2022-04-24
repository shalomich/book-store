using BookStore.Application.Exceptions;
using BookStore.Domain.Entities.Products;
using BookStore.Persistance;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Editing.UpdateDiscount;

public record UpdateDiscountCommand(int ProductId, Type ProductType, UpdateDiscountDto UpdateDiscountDto) : IRequest;
internal class UpdateDiscountHandler : AsyncRequestHandler<UpdateDiscountCommand>
{
    private ApplicationContext Context { get;}

    public UpdateDiscountHandler(ApplicationContext context)
    {
        Context = context;
    }

    protected override async Task Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var (productId, productType, updateDiscountDto) = request;

        var product = (Product) await Context.FindAsync(productType, new object[] { productId }, cancellationToken);

        if (product.DiscountPercentage == updateDiscountDto.DiscountPercentage)
        {
            throw new BadRequestException("Product has already had this discount percentage.");
        }

        product.DiscountPercentage = updateDiscountDto.DiscountPercentage;

        await Context.SaveChangesAsync(cancellationToken);
    }
}

