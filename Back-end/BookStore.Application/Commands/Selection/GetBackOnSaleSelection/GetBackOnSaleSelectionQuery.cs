using AutoMapper;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Commands.Selection.Common;
using BookStore.Application.Dto;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using Microsoft.EntityFrameworkCore;
using QueryWorker;
using System;
using System.Linq;

namespace BookStore.Application.Commands.Selection.GetBackOnSaleSelection;

public record GetBackOnSaleSelectionQuery(OptionParameters OptionParameters) : GetSelectionQuery(OptionParameters);
internal class GetBackOnSaleSelectionQueryHandler : GetSelectionQueryHandler<GetBackOnSaleSelectionQuery>
{
    public ApplicationContext Context { get; }

    public GetBackOnSaleSelectionQueryHandler(
        SelectionConfigurator<Book> selectionConfigurator,
        IMapper mapper,
        ImageFileRepository imageFileRepository,
        ApplicationContext context) : base(selectionConfigurator, mapper, imageFileRepository)
    {
        Context = context;
    }

    protected override IQueryable<Book> GetSelectionQuery(GetBackOnSaleSelectionQuery request)
    {
        const int backOnSaleDaysCount = 7;

        var now = DateTimeOffset.Now;

        var lastBackOnSaledate = now.AddDays(-backOnSaleDaysCount);

        return Context.Books
            .Where(book => book.ProductCloseout != null 
                && book.ProductCloseout.ReplenishmentDate != null
                && book.AddingDate >= lastBackOnSaledate)
            .OrderByDescending(book => book.ProductCloseout.ReplenishmentDate);
    }
}

