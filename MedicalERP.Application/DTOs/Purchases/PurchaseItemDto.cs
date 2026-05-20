namespace MedicalERP.Application.DTOs.Purchases;

public class PurchaseItemDto
{
    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitCostPrice { get; set; }

    public decimal TotalPrice { get; set; }
}