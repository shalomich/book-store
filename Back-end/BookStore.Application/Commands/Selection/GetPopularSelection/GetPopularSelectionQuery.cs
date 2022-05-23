using AutoMapper;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Commands.Selection.Common;
using BookStore.Application.Dto;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using Microsoft.Extensions.Configuration;
using QueryWorker;
using System.Linq;

namespace BookStore.Application.Commands.Selection.GetNoveltySelection
{
    public record GetPopularSelectionQuery(OptionParameters OptionParameters) : GetSelectionQuery(OptionParameters);
    internal class GetPopularSelectionQueryHandler : GetSelectionQueryHandler<GetPopularSelectionQuery>
    {
        public int OrderToViewRatio { get;  }
        public ApplicationContext Context { get; }

        public GetPopularSelectionQueryHandler(
            SelectionConfigurator<Book> selectionConfigurator,
            IMapper mapper,
            ImageFileRepository imageFileRepository,
            ApplicationContext context,
            IConfiguration configuration) : base(selectionConfigurator, mapper, imageFileRepository)
        {
            Context = context;

            OrderToViewRatio = int.Parse(configuration[nameof(OrderToViewRatio)]);
        }

        protected override IQueryable<Book> GetSelectionQuery(GetPopularSelectionQuery request)
        {
            return Context.Books
                .Select(book => new
                {
                    Book = book,
                    ViewCount = book.Views.Sum(view => view.Count),
                    OrderCount = book.OrderProducts.Sum(orderProduct => orderProduct.Quantity)
                })
                .OrderByDescending(bookInfo => bookInfo.ViewCount + bookInfo.OrderCount * OrderToViewRatio)
                .ThenByDescending(bookInfo => bookInfo.Book.AddingDate)
                .Select(bookInfo => bookInfo.Book);
        }
    }
}