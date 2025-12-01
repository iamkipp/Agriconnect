namespace Marketplace.Domain.Enums;

public enum UserRole { Buyer, Farmer, Courier, Admin }
public enum OrderStatus { Created, Paid, InTransit, Delivered, Dispute, Completed, Cancelled }
public enum EscrowStatus { Held, Released, Refunded }
public enum PremierStatus { Active, Expired, Cancelled }
public enum DeliveryStatus { Pending, InTransit, Delivered, Failed }
public enum PaymentTransactionType { Hold, Release, Capture, Refund }
