namespace Marketplace.Domain.Entities;

public class EscrowLedger
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public decimal AmountHeld { get; set; }
    public decimal PlatformFee { get; set; }
    public EscrowStatus Status { get; set; } = EscrowStatus.Held;
    public DateTime HeldAt { get; set; } = DateTime.UtcNow;
    public DateTime? ReleasedAt { get; set; }
    public string? ExternalHoldId { get; set; } // payment provider hold ID
}

