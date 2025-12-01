namespace Marketplace.Domain.Entities;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BuyerId { get; set; }
    public User Buyer { get; set; } = null!;
    public Guid FarmerId { get; set; }
    public User Farmer { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public string Currency { get; set; } = "KES";
    public OrderStatus Status { get; set; } = OrderStatus.Created;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    public Delivery? Delivery { get; set; }
    public EscrowLedger? Escrow { get; set; }
}