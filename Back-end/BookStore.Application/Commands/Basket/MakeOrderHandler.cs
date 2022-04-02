using BookStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Persistance;
using BookStore.Application.Exceptions;
using BookStore.Application.Dto;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Application.DbQueryConfigs.Specifications;
using Microsoft.EntityFrameworkCore;
using BookStore.Application.DbQueryConfigs.IncludeRequirements;

namespace BookStore.Application.Commands
{
    public record MakeOrderCommand(int UserId, OrderMakingDto OrderMaking) : IRequest<Order>;
    public class MakeOrderHandler : IRequestHandler<MakeOrderCommand, Order>
    {
        private const string EmptyBasketMessage = "Basket is empty";
        private ApplicationContext Context { get;}

        public MakeOrderHandler(ApplicationContext context)
        {
            Context = context;
        }

        public async Task<Order> Handle(MakeOrderCommand request, CancellationToken cancellationToken)
        {
            var (userId, orderMaking) = request;

            IEnumerable<BasketProduct> basketProducts = await Context.BasketProducts
                .Include(basketProduct => basketProduct.Product)
                .Where(basketProduct => basketProduct.UserId == userId)
                .ToListAsync();

            if (!basketProducts.Any())
                throw new BadRequestException(EmptyBasketMessage);

            ISet<OrderProduct> orderProducts = basketProducts
                .Select(basketProduct => new OrderProduct
                {
                    Name = basketProduct.Product.Name,
                    Cost = basketProduct.Product.Cost,
                    Quantity = basketProduct.Quantity,
                    ProductId = basketProduct.ProductId
                })
                .ToHashSet();

            return new Order
            {
                UserId = userId,
                Products = orderProducts,
                UserName = orderMaking.UserName,
                Email = orderMaking.Email,
                PhoneNumber = orderMaking.PhoneNumber,
                Address = orderMaking.Address,
                OrderReceiptMethod = orderMaking.OrderReceiptMethod,
                PaymentMethod = orderMaking.PaymentMethod
            };
        }
    }
}
