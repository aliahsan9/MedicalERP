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
    private readonly IAuditService _auditService;

    public SaleService(
        ApplicationDbContext context,
        IAuditService auditService)
    {
        _context = context;
        _auditService = auditService;
    }

    public async Task<SaleDto> CreateSaleAsync(CreateSaleRequest request)
    {
        if (request.Items == null || !request.Items.Any())
            throw new Exception("Sale must contain at least one item");

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = GenerateInvoiceNumber(),
                CustomerName = string.IsNullOrWhiteSpace(request.CustomerName)
                    ? "Walk-In Customer"
                    : request.CustomerName,
                CreatedAt = DateTime.UtcNow,
                Items = new List<SaleItem>()
            };

            decimal total = 0;

            foreach (var item in request.Items)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(x => x.Id == item.ProductId);

                if (product == null)
                    throw new Exception("Product not found");

                if (product.StockQuantity < item.Quantity)
                    throw new Exception($"Insufficient stock for {product.Name}");

                product.StockQuantity -= item.Quantity;

                var saleItem = new SaleItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    TotalPrice = product.Price * item.Quantity
                };

                sale.Items.Add(saleItem);

                _context.InventoryTransactions.Add(new InventoryTransaction
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    Type = InventoryTransactionType.StockOut,
                    Notes = $"Sale {sale.InvoiceNumber}"
                });

                total += saleItem.TotalPrice;
            }

            sale.TotalAmount = total;

            _context.Sales.Add(sale);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            // =========================
            // AUDIT LOG
            // =========================
            await _auditService.LogAsync(
                userId: "SYSTEM",
                action: "SALE_CREATED",
                entityName: "Sale",
                entityId: sale.Id.ToString(),
                newValues: new
                {
                    sale.InvoiceNumber,
                    sale.TotalAmount,
                    sale.CustomerName
                }
            );

            return await GetSaleByIdInternalAsync(sale.Id);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<SaleDto>> GetAllAsync()
    {
        return await _context.Sales
            .AsNoTracking()
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new SaleDto
            {
                Id = x.Id,
                InvoiceNumber = x.InvoiceNumber,
                CustomerName = x.CustomerName,
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
        return await GetSaleByIdInternalAsync(id);
    }

    private async Task<SaleDto> GetSaleByIdInternalAsync(Guid id)
    {
        var sale = await _context.Sales
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (sale == null)
            throw new Exception("Sale not found");

        return new SaleDto
        {
            Id = sale.Id,
            InvoiceNumber = sale.InvoiceNumber,
            CustomerName = sale.CustomerName,
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
        => $"INV-{DateTime.UtcNow:yyyyMMddHHmmssfff}";
}