using BookStore.Application.Extensions;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Application.Services.CatalogSelections;
public class SpecialForYouCategorySelection : IBookSelection
{
    private ApplicationContext Context { get;}
    private LoggedUserAccessor LoggedUserAccessor { get; }
    public int? TagCount { get; set; }

    public SpecialForYouCategorySelection(ApplicationContext context, LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }

    public IQueryable<Book> Select()
    {
        int currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var userOrderProductIds = Context.Orders
            .Where(order => order.UserId == currentUserId)
            .SelectMany(order => order.Products)
            .Select(orderProduct => orderProduct.Product.Id)
            .ToArray();

        var userOrderTags = Context.Tags
            .Where(tag => tag.ProductTags
            .Any(productTag => userOrderProductIds.Contains(productTag.ProductId)));

        if (TagCount == null)
            TagCount = userOrderTags.Distinct().Count();

        var selectionTagIds = userOrderTags
            .GroupBy(tag => tag.Id)
            .Select(tagGroup => new { Id = tagGroup.Key, Count = tagGroup.Count() })
            .OrderByDescending(tagIdAndCount => tagIdAndCount.Count)
            .Select(tagIdAndCount => tagIdAndCount.Id)
            .Take(TagCount.Value)
            .ToArray();

        return Context.Books
            .Where(book => book.ProductTags
            .Any(productTag => selectionTagIds.Contains(productTag.TagId))
                && !userOrderProductIds.Contains(book.Id))
            .Shuffle();
    }
}
