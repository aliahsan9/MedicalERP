namespace MedicalERP.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;
    // e.g. Tablet, Syrup, Injection

    public decimal Price { get; set; }

    public decimal CostPrice { get; set; }

    public int StockQuantity { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? Barcode { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<InventoryTransaction> InventoryTransactions { get; set; }
    = new List<InventoryTransaction>();
    public ICollection<SaleItem> SaleItems { get; set; }
    = new List<SaleItem>();
}