namespace Marketplace.Domain.Entities;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FarmerId { get; set; }
    public User Farmer { get; set; } = null!;
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public string Unit { get; set; } = "kg";
    public int QuantityAvailable { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<PremierBoost> PremierBoosts { get; set; } = new List<PremierBoost>();
}
