namespace MedicalERP.Application.DTOs.Sales;

public class CreateSaleRequest
{
    public List<CreateSaleItemRequest> Items { get; set; }
        = new();
}