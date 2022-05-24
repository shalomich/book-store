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

namespace BookStore.Application.Commands.Selection.GetNoveltySelection;

public record GetSpecialForYouSelectionQuery(OptionParameters OptionParameters) : GetSelectionQuery(OptionParameters);
internal class GetSpecialForYouSelectionQueryHandler : GetSelectionQueryHandler<GetSpecialForYouSelectionQuery>
{
    private ApplicationContext Context { get; }
    private LoggedUserAccessor LoggedUserAccessor { get; }

    public GetSpecialForYouSelectionQueryHandler(
        SelectionConfigurator<Book> selectionConfigurator,
        IMapper mapper,
        ImageFileRepository imageFileRepository,
        ApplicationContext context,
        LoggedUserAccessor loggedUserAccessor) : base(selectionConfigurator, mapper, imageFileRepository)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }

    protected override IQueryable<Book> GetSelectionQuery(GetSpecialForYouSelectionQuery request)
    {
       var currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var userTags = Context.Tags
         .Where(tag => tag.Users
             .Any(user => user.Id == currentUserId));

        return Context.Books
            .Where(book => book.ProductTags
                .Any(productTag => userTags.Any(tag => tag.Id == productTag.TagId)))
            .Select(book => new
            {
                Book = book,
                TagCount = book.ProductTags
                    .Select(productTag => productTag.Tag)
                    .Intersect(userTags)
                    .Count()
            })
            .OrderByDescending(bookInfo => bookInfo.TagCount)
            .ThenByDescending(bookInfo => bookInfo.Book.AddingDate)
            .Select(bookInfo => bookInfo.Book);
    }
}

