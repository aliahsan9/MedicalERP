using MedicalERP.Domain.Enums;

namespace MedicalERP.Application.DTOs.Inventory;

public class InventoryTransactionDto
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public InventoryTransactionType Type { get; set; }

    public int Quantity { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }
}