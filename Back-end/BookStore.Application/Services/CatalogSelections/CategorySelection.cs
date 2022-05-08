using BookStore.Application.DbQueryConfigs.CategoryFactories;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using BookStore.Application.Extensions;
using BookStore.Persistance;

namespace BookStore.Application.Services.CatalogSelections;
public class CategorySelection : IBookSelection
{
    private ApplicationContext Context { get; }

    public Category? ChoosenCategory { get; set; }

    public CategorySelection(ApplicationContext context)
    {
        Context = context;
    }

    public IQueryable<Book> Select()
    {
        IQueryable<Book> categoryBooks = Context.Books;

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
