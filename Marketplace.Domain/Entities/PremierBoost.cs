namespace Marketplace.Domain.Entities;

public class PremierBoost
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public Guid FarmerId { get; set; }
    public decimal AmountPaid { get; set; }
    public int PriorityWeight { get; set; } = 100;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public PremierStatus Status { get; set; } = PremierStatus.Active;
    public string? PaymentTransactionId { get; set; }
}
