namespace MedicalERP.Application.DTOs.Purchases;

public class CreatePurchaseRequest
{
    public Guid SupplierId { get; set; }

    public List<CreatePurchaseItemRequest> Items { get; set; }
        = new();
}