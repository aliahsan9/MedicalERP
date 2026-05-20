namespace MedicalERP.Application.DTOs.Dashboard;

public class DashboardSummaryDto
{
    public decimal TodaySales { get; set; }

    public decimal MonthlySales { get; set; }

    public decimal TotalPurchases { get; set; }

    public int TotalProducts { get; set; }

    public int LowStockProducts { get; set; }

    public int ExpiringProducts { get; set; }
}