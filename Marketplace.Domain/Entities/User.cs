using Marketplace.Domain.Enums;

namespace Marketplace.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public UserRole Role { get; set; } = UserRole.Buyer;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public FarmerProfile? FarmerProfile { get; set; }
}
