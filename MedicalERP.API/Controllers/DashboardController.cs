using MedicalERP.Application.Interfaces;
using MedicalERP.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalERP.API.Controllers;

[ApiController]
[Route("api/dashboard")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    // ========================================
    // DASHBOARD SUMMARY
    // ========================================

    [HttpGet("summary")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.Doctor}")]
    public async Task<IActionResult> GetSummary()
    {
        var result = await _dashboardService.GetSummaryAsync();

        return Ok(result);
    }

    // ========================================
    // SALES REPORT
    // ========================================

    [HttpGet("sales-report")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> GetSalesReport()
    {
        var result = await _dashboardService.GetDailySalesReportAsync();

        return Ok(result);
    }

    // ========================================
    // LOW STOCK
    // ========================================

    [HttpGet("low-stock")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.Doctor}")]
    public async Task<IActionResult> GetLowStock()
    {
        var result = await _dashboardService.GetLowStockProductsAsync();

        return Ok(result);
    }

    // ========================================
    // EXPIRING PRODUCTS
    // ========================================

    [HttpGet("expiring-products")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.Doctor}")]
    public async Task<IActionResult> GetExpiringProducts()
    {
        var result = await _dashboardService.GetExpiringProductsAsync();

        return Ok(result);
    }
}