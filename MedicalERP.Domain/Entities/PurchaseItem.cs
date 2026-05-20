namespace MedicalERP.Domain.Entities;

public class PurchaseItem
{
    public Guid Id { get; set; }

    public Guid PurchaseId { get; set; }

    public Purchase Purchase { get; set; } = default!;

    public Guid ProductId { get; set; }

    public Product Product { get; set; } = default!;

    public int Quantity { get; set; }

    public decimal UnitCostPrice { get; set; }

    public decimal TotalPrice { get; set; }
}