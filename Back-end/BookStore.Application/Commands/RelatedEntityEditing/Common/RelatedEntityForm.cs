using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.RelatedEntityEditing.Common;
public record RelatedEntityForm
{
    public int Id { get; init; }

    [Required]
    public virtual string Name { get; init; }
}

