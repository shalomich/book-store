using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Orders.PlaceOrder;

public record PlaceOrderCommand(OrderForm Order) : IRequest<int>;
internal class PlaceOrderHandler : IRequestHandler<PlaceOrderCommand, int>
{
    private ApplicationContext Context { get; }
    private LoggedUserAccessor LoggedUserAccessor { get; }
    private IMapper Mapper { get; }

    public PlaceOrderHandler(ApplicationContext context, LoggedUserAccessor loggedUserAccessor, IMapper mapper)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
        Mapper = mapper;
    }

    public async Task<int> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var order = Mapper.Map<Order>(request.Order);

        int currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var orderProducts = await Context.BasketProducts
            .Where(basketProduct => basketProduct.UserId == currentUserId)
            .ProjectTo<OrderProduct>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        order.UserId = currentUserId;
        order.Products = orderProducts.ToHashSet();
        order.PlacedDate = DateTimeOffset.Now;

        Context.Add(order);
        await Context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }

    private async Task Validate(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        int currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var userBasketProductQuery = Context.BasketProducts
            .Where(basketProduct => basketProduct.UserId == currentUserId);

        bool isFullBasket = await userBasketProductQuery.AnyAsync(cancellationToken);

        if (!isFullBasket)
        {
            throw new BadRequestException("Basket is empty");
        }

        var excessiveBasketProduct = await userBasketProductQuery
            .Where(basketProduct => basketProduct.Quantity > basketProduct.Product.Quantity)
            .Select(basketProduct => new 
            { 
                ProductCount = basketProduct.Product.Quantity,
                BasketProductCount = basketProduct.Quantity,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (excessiveBasketProduct != null)
        {
            throw new BadRequestException($"Basket product quantity ({excessiveBasketProduct.BasketProductCount}) " +
                $"more than product quantity ({excessiveBasketProduct.ProductCount})");
        }
    }
}

