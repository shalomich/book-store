using BookStore.Application.DbQueryConfigs.Specifications;
using BookStore.Application.Exceptions;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries
{
    public record CheckBasketProductQuantiesQuery(int UserId) : IRequest<Unit>;
    public class CheckBasketProductQuantiesHandler : IRequestHandler<CheckBasketProductQuantiesQuery, Unit>
    {
        private const string InvalidQuantityMessage = "Basket product quantity ({0}) more than product quantity ({1})";
        private DbEntityQueryBuilder<BasketProduct> BasketProductQueryBuilder { get; }

        public CheckBasketProductQuantiesHandler(DbEntityQueryBuilder<BasketProduct> basketProductQueryBuilder)
        {
            BasketProductQueryBuilder = basketProductQueryBuilder ?? throw new ArgumentNullException(nameof(basketProductQueryBuilder));
        }

        public async Task<Unit> Handle(CheckBasketProductQuantiesQuery request, CancellationToken cancellationToken)
        {
            BasketProduct invalidBasketProduct = await BasketProductQueryBuilder
                .AddSpecification(new BasketProductByUserIdSpecification(request.UserId))
                .AddSpecification(new MoreBasketProductQuantitySpecification())
                .Build()
                .FirstOrDefaultAsync();

            if (invalidBasketProduct != null)
                throw new BadRequestException(string.Format(
                    InvalidQuantityMessage,
                    invalidBasketProduct.Quantity.ToString(),
                    invalidBasketProduct.Quantity.ToString()));

            return Unit.Value;
        }
    }
}
