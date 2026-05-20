using MedicalERP.Application.DTOs.Purchases;
using MedicalERP.Application.Interfaces;
using MedicalERP.Domain.Entities;
using MedicalERP.Domain.Enums;
using MedicalERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicalERP.Infrastructure.Services;

public class PurchaseService : IPurchaseService
{
    private readonly ApplicationDbContext _context;

    public PurchaseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PurchaseDto> CreatePurchaseAsync(
        CreatePurchaseRequest request)
    {
        var supplier = await _context.Suppliers
            .FirstOrDefaultAsync(x => x.Id == request.SupplierId);

        if (supplier == null)
            throw new Exception("Supplier not found");

        var purchase = new Purchase
        {
            Id = Guid.NewGuid(),
            SupplierId = supplier.Id,
            InvoiceNumber = GenerateInvoiceNumber()
        };

        decimal totalAmount = 0;

        foreach (var item in request.Items)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == item.ProductId);

            if (product == null)
                throw new Exception("Product not found");

            // INCREASE STOCK
            product.StockQuantity += item.Quantity;

            // UPDATE COST PRICE
            product.CostPrice = item.UnitCostPrice;

            // INVENTORY TRANSACTION
            var inventoryTransaction = new InventoryTransaction
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Type = InventoryTransactionType.StockIn,
                Quantity = item.Quantity,
                Notes = $"Purchase Invoice: {purchase.InvoiceNumber}"
            };

            _context.InventoryTransactions.Add(
                inventoryTransaction);

            var totalPrice =
                item.UnitCostPrice * item.Quantity;

            var purchaseItem = new PurchaseItem
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitCostPrice = item.UnitCostPrice,
                TotalPrice = totalPrice
            };

            totalAmount += totalPrice;

            purchase.Items.Add(purchaseItem);
        }

        purchase.TotalAmount = totalAmount;

        _context.Purchases.Add(purchase);

        await _context.SaveChangesAsync();

        return await GetPurchaseDto(purchase.Id);
    }

    public async Task<List<PurchaseDto>> GetAllAsync()
    {
        return await _context.Purchases
            .Include(x => x.Supplier)
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new PurchaseDto
            {
                Id = x.Id,
                InvoiceNumber = x.InvoiceNumber,
                SupplierName = x.Supplier.Name,
                TotalAmount = x.TotalAmount,
                CreatedAt = x.CreatedAt,

                Items = x.Items.Select(i => new PurchaseItemDto
                {
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    UnitCostPrice = i.UnitCostPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<PurchaseDto?> GetByIdAsync(Guid id)
    {
        return await GetPurchaseDto(id);
    }

    private async Task<PurchaseDto> GetPurchaseDto(Guid id)
    {
        var purchase = await _context.Purchases
            .Include(x => x.Supplier)
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .FirstAsync(x => x.Id == id);

        return new PurchaseDto
        {
            Id = purchase.Id,
            InvoiceNumber = purchase.InvoiceNumber,
            SupplierName = purchase.Supplier.Name,
            TotalAmount = purchase.TotalAmount,
            CreatedAt = purchase.CreatedAt,

            Items = purchase.Items.Select(i =>
                new PurchaseItemDto
                {
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    UnitCostPrice = i.UnitCostPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
        };
    }

    private string GenerateInvoiceNumber()
    {
        return $"PUR-{DateTime.UtcNow:yyyyMMddHHmmss}";
    }
}