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

namespace BookStore.Application.Commands.Selection.GetLastViewedSelection;

public record GetLastViewedSelectionQuery(OptionParameters OptionParameters) : GetSelectionQuery(OptionParameters);
internal class GetLastViewedSelectionQueryHandler : GetSelectionQueryHandler<GetLastViewedSelectionQuery>
{
    public ApplicationContext Context { get; }
    public LoggedUserAccessor LoggedUserAccessor { get; }

    public GetLastViewedSelectionQueryHandler(
        SelectionConfigurator<Book> selectionConfigurator,
        IMapper mapper,
        ImageFileRepository imageFileRepository,
        ApplicationContext context,
        LoggedUserAccessor loggedUserAccessor) : base(selectionConfigurator, mapper, imageFileRepository)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }

    protected override IQueryable<Book> GetSelectionQuery(GetLastViewedSelectionQuery request)
    {
        var currentUserId = LoggedUserAccessor.GetCurrentUserId();

        return Context.Views
            .Where(view => view.UserId == currentUserId)
            .OrderByDescending(view => view.LastViewDate)
            .Select(view => view.Book);
    }
}

