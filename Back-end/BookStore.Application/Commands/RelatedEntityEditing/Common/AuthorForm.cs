using BookStore.Domain.Entities.Books;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.RelatedEntityEditing.Common;
public record AuthorForm : RelatedEntityForm
{
    [RegularExpression(Author.NameMask, ErrorMessage = Author.NameSchema)]
    public override string Name { get; init; }
}

