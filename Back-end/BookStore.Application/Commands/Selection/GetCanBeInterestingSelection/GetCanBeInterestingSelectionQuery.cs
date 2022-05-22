using AutoMapper;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Commands.Selection.Common;
using BookStore.Application.Dto;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using QueryWorker;
using System;
using System.Linq;

namespace BookStore.Application.Commands.Selection.GetCanBeInterestingSelection;

public record GetCanBeInterestingSelectionQuery(int? TagCount, OptionParameters OptionParameters) : GetSelectionQuery(OptionParameters);
internal class GetCanBeInterestingSelectionQueryHandler : GetSelectionQueryHandler<GetCanBeInterestingSelectionQuery>
{
    public ApplicationContext Context { get; }
    public LoggedUserAccessor LoggedUserAccessor { get; }

    public GetCanBeInterestingSelectionQueryHandler(
        SelectionConfigurator<Book> selectionConfigurator,
        IMapper mapper,
        ImageFileRepository imageFileRepository,
        ApplicationContext context,
        LoggedUserAccessor loggedUserAccessor) : base(selectionConfigurator, mapper, imageFileRepository)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }

    protected override IQueryable<Book> GetSelectionQuery(GetCanBeInterestingSelectionQuery request)
    {
        var tagCount = request.TagCount;

        int currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var booksByUserOrders = Context.Books
            .Where(book => book.OrderProducts
                .Select(orderProduct => orderProduct.Order)
                .Any(order => order.UserId == currentUserId));

        var tagsByUserOrders = booksByUserOrders
            .SelectMany(book => book.ProductTags)
            .Select(productTag => productTag.Tag)
            .GroupBy(tag => tag.Id)
            .OrderByDescending(tagGroup => tagGroup.Count())
            .Select(tagGroup => tagGroup.Key);
        
        if (tagCount.HasValue)
        {
            tagsByUserOrders = tagsByUserOrders.Take(tagCount.Value);
        }

        var selectionTagIds = tagsByUserOrders.ToList();

        if (!selectionTagIds.Any())
        {
            return Context.Books
                .Where(book => book.ProductTags
                    .Any(productTag => selectionTagIds.Contains(productTag.TagId)));
        }

        IQueryable<Book> selectionBooks = null;

        Func<int, IQueryable<Book>> selectionBooksByTagId = tagId => Context.Books
            .Except(booksByUserOrders)
            .Where(book => book.ProductTags
                .Any(productTag => productTag.TagId == tagId));

        foreach (var tagId in selectionTagIds)
        {
            selectionBooks = selectionBooks != null
                ? selectionBooks.Concat(selectionBooksByTagId(tagId))
                : selectionBooksByTagId(tagId);
        }

        return selectionBooks;
    }
}

