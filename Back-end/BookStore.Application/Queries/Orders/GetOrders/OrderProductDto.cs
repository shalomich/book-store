namespace BookStore.Application.Queries.Orders.GetOrders;
public record OrderProductDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int Cost { get; set; }
}

