namespace MedicalERP.Application.DTOs.Sales;

public class CreateSaleItemRequest
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}