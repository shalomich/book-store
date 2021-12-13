using System.ComponentModel.DataAnnotations;

public record BasketProductAddView
{
    [Required]
    public int? ProductId { init; get; }
}

