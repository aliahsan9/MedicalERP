using MedicalERP.Domain.Enums;

namespace MedicalERP.Application.DTOs.Inventory;

public class CreateInventoryTransactionRequest
{
    public Guid ProductId { get; set; }

    public InventoryTransactionType Type { get; set; }

    public int Quantity { get; set; }

    public string? Notes { get; set; }
}