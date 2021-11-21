using BookStore.Domain.Entities.Books;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Queries.Selections
{
    public interface ISelectionQuery : IRequest<IEnumerable<Book>>
    {
    }
}
