namespace MedicalERP.Application.DTOs.Invoice;

public class InvoiceDto
{
    public Guid SaleId { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public string CustomerName { get; set; } = "Walk-In Customer";

    public DateTime Date { get; set; }

    public decimal GrandTotal { get; set; }

    public List<InvoiceItemDto> Items { get; set; } = new();
}