using MedicalERP.Application.DTOs.Dashboard;
using MedicalERP.Application.Interfaces;
using MedicalERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicalERP.Infrastructure.Services;

public class DashboardService : IDashboardService
{
    private readonly ApplicationDbContext _context;

    public DashboardService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync()
    {
        var today = DateTime.UtcNow.Date;

        var todaySales = await _context.Sales
            .Where(x => x.CreatedAt.Date == today)
            .SumAsync(x => (decimal?)x.TotalAmount) ?? 0;

        var monthlySales = await _context.Sales
            .Where(x => x.CreatedAt.Month == today.Month &&
                        x.CreatedAt.Year == today.Year)
            .SumAsync(x => (decimal?)x.TotalAmount) ?? 0;

        var totalPurchases = await _context.Purchases
            .SumAsync(x => (decimal?)x.TotalAmount) ?? 0;

        var totalProducts = await _context.Products.CountAsync();

        var lowStockProducts = await _context.Products
            .CountAsync(x => x.StockQuantity <= 10);

        var expiringProducts = await _context.Products
            .CountAsync(x => x.ExpiryDate <= today.AddDays(30));

        return new DashboardSummaryDto
        {
            TodaySales = todaySales,
            MonthlySales = monthlySales,
            TotalPurchases = totalPurchases,
            TotalProducts = totalProducts,
            LowStockProducts = lowStockProducts,
            ExpiringProducts = expiringProducts
        };
    }

    public async Task<List<SalesReportDto>> GetDailySalesReportAsync()
    {
        var last7Days = DateTime.UtcNow.Date.AddDays(-7);

        return await _context.Sales
            .Where(x => x.CreatedAt >= last7Days)
            .GroupBy(x => x.CreatedAt.Date)
            .Select(g => new SalesReportDto
            {
                Date = g.Key,
                TotalSales = g.Sum(x => x.TotalAmount),
                TotalInvoices = g.Count()
            })
            .OrderBy(x => x.Date)
            .ToListAsync();
    }

    public async Task<List<LowStockDto>> GetLowStockProductsAsync()
    {
        return await _context.Products
            .Where(x => x.StockQuantity <= 10)
            .Select(x => new LowStockDto
            {
                ProductId = x.Id,
                ProductName = x.Name,
                Quantity = x.StockQuantity
            })
            .ToListAsync();
    }

    public async Task<List<ExpiringProductDto>> GetExpiringProductsAsync()
    {
        var today = DateTime.UtcNow.Date;

        return await _context.Products
            .Where(x => x.ExpiryDate <= today.AddDays(30))
            .Select(x => new ExpiringProductDto
            {
                ProductId = x.Id,
                ProductName = x.Name,
                ExpiryDate = x.ExpiryDate,
                DaysRemaining =
                    EF.Functions.DateDiffDay(today, x.ExpiryDate)
            })
            .OrderBy(x => x.DaysRemaining)
            .ToListAsync();
    }
}