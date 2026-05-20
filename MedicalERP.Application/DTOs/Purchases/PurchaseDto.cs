namespace MedicalERP.Application.DTOs.Purchases;

public class PurchaseDto
{
    public Guid Id { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public string SupplierName { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<PurchaseItemDto> Items { get; set; }
        = new();
}