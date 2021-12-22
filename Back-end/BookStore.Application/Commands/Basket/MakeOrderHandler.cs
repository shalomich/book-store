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
        private DbEntityQueryBuilder<BasketProduct> BasketProductQueryBuilder { get; }

        public MakeOrderHandler(DbEntityQueryBuilder<BasketProduct> basketProductQueryBuilder)
        {
            BasketProductQueryBuilder = basketProductQueryBuilder ?? throw new ArgumentNullException(nameof(basketProductQueryBuilder));
        }

        public async Task<Order> Handle(MakeOrderCommand request, CancellationToken cancellationToken)
        {
            var (userId, orderMaking) = request;

            IEnumerable<BasketProduct> basketProducts = await BasketProductQueryBuilder
                .AddSpecification(new BasketProductByUserIdSpecification(userId))
                .AddIncludeRequirements(new BasketProductIncludeRequirement())
                .Build()
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
                PhoneNumber = orderMaking.PhoneNumber
            };
        }
    }
}
