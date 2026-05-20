using MedicalERP.Application.DTOs.Dashboard;

namespace MedicalERP.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetSummaryAsync();

    Task<List<SalesReportDto>> GetDailySalesReportAsync();

    Task<List<LowStockDto>> GetLowStockProductsAsync();

    Task<List<ExpiringProductDto>> GetExpiringProductsAsync();
}