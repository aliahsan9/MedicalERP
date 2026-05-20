namespace MedicalERP.Application.DTOs.Sales;

public class SaleDto
{
    public Guid Id { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<SaleItemDto> Items { get; set; }
        = new();
}