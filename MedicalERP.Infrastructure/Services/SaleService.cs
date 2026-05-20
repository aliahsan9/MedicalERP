using MedicalERP.Application.DTOs.Sales;
using MedicalERP.Application.Interfaces;
using MedicalERP.Domain.Entities;
using MedicalERP.Domain.Enums;
using MedicalERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicalERP.Infrastructure.Services;

public class SaleService : ISaleService
{
    private readonly ApplicationDbContext _context;

    public SaleService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SaleDto> CreateSaleAsync(CreateSaleRequest request)
    {
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            InvoiceNumber = GenerateInvoiceNumber()
        };

        decimal totalAmount = 0;

        foreach (var item in request.Items)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == item.ProductId);

            if (product == null)
                throw new Exception("Product not found");

            if (product.StockQuantity < item.Quantity)
                throw new Exception(
                    $"Insufficient stock for {product.Name}");

            // REDUCE STOCK
            product.StockQuantity -= item.Quantity;

            // INVENTORY TRANSACTION
            var inventoryTransaction = new InventoryTransaction
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Type = InventoryTransactionType.StockOut,
                Quantity = item.Quantity,
                Notes = $"Sale Invoice: {sale.InvoiceNumber}"
            };

            _context.InventoryTransactions.Add(
                inventoryTransaction);

            var totalPrice = product.Price * item.Quantity;

            var saleItem = new SaleItem
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price,
                TotalPrice = totalPrice
            };

            totalAmount += totalPrice;

            sale.Items.Add(saleItem);
        }

        sale.TotalAmount = totalAmount;

        _context.Sales.Add(sale);

        await _context.SaveChangesAsync();

        return await GetSaleDto(sale.Id);
    }

    public async Task<List<SaleDto>> GetAllAsync()
    {
        return await _context.Sales
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new SaleDto
            {
                Id = x.Id,
                InvoiceNumber = x.InvoiceNumber,
                TotalAmount = x.TotalAmount,
                CreatedAt = x.CreatedAt,

                Items = x.Items.Select(i => new SaleItemDto
                {
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<SaleDto?> GetByIdAsync(Guid id)
    {
        return await GetSaleDto(id);
    }

    private async Task<SaleDto> GetSaleDto(Guid saleId)
    {
        var sale = await _context.Sales
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .FirstAsync(x => x.Id == saleId);

        return new SaleDto
        {
            Id = sale.Id,
            InvoiceNumber = sale.InvoiceNumber,
            TotalAmount = sale.TotalAmount,
            CreatedAt = sale.CreatedAt,

            Items = sale.Items.Select(i => new SaleItemDto
            {
                ProductName = i.Product.Name,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                TotalPrice = i.TotalPrice
            }).ToList()
        };
    }

    private string GenerateInvoiceNumber()
    {
        return $"INV-{DateTime.UtcNow:yyyyMMddHHmmss}";
    }
}