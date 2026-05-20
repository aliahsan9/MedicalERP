namespace MedicalERP.Application.DTOs.Purchases;

public class CreatePurchaseItemRequest
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitCostPrice { get; set; }
}