namespace MedicalERP.Domain.Entities;

public class Sale
{
    public Guid Id { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = "Walk-In Customer";

    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<SaleItem> Items { get; set; }
        = new List<SaleItem>();
}