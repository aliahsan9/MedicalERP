using MedicalERP.Domain.Enums;

namespace MedicalERP.Domain.Entities;

public class InventoryTransaction
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public Product Product { get; set; } = default!;

    public InventoryTransactionType Type { get; set; }

    public int Quantity { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}