namespace MedicalERP.Domain.Entities;

public class Purchase
{
    public Guid Id { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public Guid SupplierId { get; set; }

    public Supplier Supplier { get; set; } = default!;

    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;

    public ICollection<PurchaseItem> Items { get; set; }
        = new List<PurchaseItem>();
}