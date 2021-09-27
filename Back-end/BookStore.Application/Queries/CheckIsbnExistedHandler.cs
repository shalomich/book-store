using BookStore.Application.Exceptions;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries
{
    public record CheckIsbnExistedQuery(string Isbn) : IRequest<bool>;
    public class CheckIsbnExistedHandler : IRequestHandler<CheckIsbnExistedQuery, bool>
    {
        private const string EmptyIsbnMessage = "Isbn is empty";
        private ApplicationContext Context { get; }

        public CheckIsbnExistedHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<bool> Handle(CheckIsbnExistedQuery request, CancellationToken cancellationToken)
        {
            string isbn = request.Isbn;

            if (string.IsNullOrEmpty(isbn))
                throw new BadRequestException(EmptyIsbnMessage);

            if (Regex.IsMatch(isbn, Book.IsbnTemplate) == false)
                throw new BadRequestException(Book.IsbnSchema);

            bool isExisted = Context.Books.Any(book => book.Isbn == request.Isbn);

            return Task.FromResult(isExisted);
        }
    }
}
