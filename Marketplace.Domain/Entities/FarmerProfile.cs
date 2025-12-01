Using marketplace.Domain.enums;

Namespace Marketplace.Domain.Entities;
public class FarmerProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string FarmName { get; set; } = "";
    public string Location { get; set; } = "";
    public bool Verified { get; set; } = false;
}