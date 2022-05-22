using AutoMapper;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Commands.Selection.Common;
using BookStore.Application.Dto;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using QueryWorker;
using System;
using System.Linq;

namespace BookStore.Application.Commands.Selection.GetGoneOnSaleSelection;

public record GetGoneOnSaleSelectionQuery(OptionParameters OptionParameters) : GetSelectionQuery(OptionParameters);
internal class GetGoneOnSaleSelectionQueryHandler : GetSelectionQueryHandler<GetGoneOnSaleSelectionQuery>
{
    public ApplicationContext Context { get; }

    public GetGoneOnSaleSelectionQueryHandler(
        SelectionConfigurator<Book> selectionConfigurator,
        IMapper mapper,
        ImageFileRepository imageFileRepository,
        ApplicationContext context) : base(selectionConfigurator, mapper, imageFileRepository)
    {
        Context = context;
    }

    protected override IQueryable<Book> GetSelectionQuery(GetGoneOnSaleSelectionQuery request)
    {
        const int goneOnSaleDaysCount = 7;

        var lastGoneOnSaledate = DateTime.Now.AddDays(-goneOnSaleDaysCount);
  
        return Context.Books
            .Where(book => book.AddingDate >= lastGoneOnSaledate)
            .OrderByDescending(book => book.AddingDate); 
    }
}

