namespace MedicalERP.Application.DTOs.Dashboard;

public class SalesReportDto
{
    public DateTime Date { get; set; }

    public decimal TotalSales { get; set; }

    public int TotalInvoices { get; set; }
}