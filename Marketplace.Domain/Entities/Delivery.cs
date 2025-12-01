namespace Marketplace.Domain.Entities;

public class Delivery
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public Guid? CourierId { get; set; }
    public string? TrackingId { get; set; }
    public DeliveryStatus Status { get; set; } = DeliveryStatus.Pending;
    public string? ProofPhotoUrl { get; set; }
    public string? ProofOtp { get; set; }
    public string? ProofGps { get; set; }
    public DateTime? DeliveredAt { get; set; }
}
