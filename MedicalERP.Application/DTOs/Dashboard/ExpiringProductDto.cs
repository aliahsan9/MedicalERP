namespace MedicalERP.Application.DTOs.Dashboard;

public class ExpiringProductDto
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public DateTime ExpiryDate { get; set; }

    public int DaysRemaining { get; set; }
}