using BookStore.Application.DbQueryConfigs.Specifications;
using BookStore.Application.Exceptions;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
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
    public record CheckBasketProductQuantityQuery(BasketProduct BasketProduct) : IRequest<Unit>;
    public class CheckBasketProductQuantityHandler : IRequestHandler<CheckBasketProductQuantityQuery, Unit>
    {
        private const string InvalidQuantityMessage = "Basket product quantity ({0}) more than product quantity ({1})";
        private ApplicationContext Context { get; }

        public CheckBasketProductQuantityHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Unit> Handle(CheckBasketProductQuantityQuery request, CancellationToken cancellationToken)
        {
            var basketProduct = request.BasketProduct;

            var product = (Product) await Context.Set<Product>()
                .FirstOrDefaultAsync(new EntityByIdSpecification(basketProduct.ProductId).ToExpression());

            if (product.Quantity < basketProduct.Quantity)
                throw new BadRequestException(string.Format(
                    InvalidQuantityMessage,
                    basketProduct.Quantity.ToString(),
                    product.Quantity.ToString()));

            return Unit.Value;
        }
    }
}
