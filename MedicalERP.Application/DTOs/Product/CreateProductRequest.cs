namespace MedicalERP.Application.DTOs.Product;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal CostPrice { get; set; }
    public int StockQuantity { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? Barcode { get; set; }
}