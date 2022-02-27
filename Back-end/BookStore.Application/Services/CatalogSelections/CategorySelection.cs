using BookStore.Application.DbQueryConfigs.CategoryFactories;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Application.Extensions;

namespace BookStore.Application.Services.CatalogSelections
{
    public class CategorySelection : ICatalogSelection
    {
        public Category? ChoosenCategory { get; set; }
        public IQueryable<Book> Select(DbSet<Book> bookSet)
        {
            IQueryable<Book> categoryBooks = bookSet;

            if (ChoosenCategory == null)
                return categoryBooks;

            ICategoryFactory selectionFactory = ChooseFactory(ChoosenCategory.Value);

            return categoryBooks
              .Where(selectionFactory.CreateSpecification()
                .ToExpression())
              .OrderBy(selectionFactory.CreateOrder());
        }

        private ICategoryFactory ChooseFactory(Category category) => category switch
        {
            Category.Novelty => new NoveltyFactory(),
            Category.GoneOnSale => new GoneOnSaleFactory(),
            Category.BackOnSale => new BackOnSaleFactory(),
            Category.ForChildren => new ForChildrenFactory(),
            Category.CurrentDayAuthor => new CurrentDayAuthorFactory(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
