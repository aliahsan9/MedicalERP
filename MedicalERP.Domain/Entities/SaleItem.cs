namespace MedicalERP.Domain.Entities;

public class SaleItem
{
    public Guid Id { get; set; }

    public Guid SaleId { get; set; }

    public Sale Sale { get; set; } = default!;

    public Guid ProductId { get; set; }

    public Product Product { get; set; } = default!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }
}