using MedicalERP.Application.DTOs.Inventory;
using MedicalERP.Application.Interfaces;
using MedicalERP.Domain.Entities;
using MedicalERP.Domain.Enums;
using MedicalERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicalERP.Infrastructure.Services;

public class InventoryService : IInventoryService
{
    private readonly ApplicationDbContext _context;
    private readonly IAuditService _auditService;

    public InventoryService(
        ApplicationDbContext context,
        IAuditService auditService)
    {
        _context = context;
        _auditService = auditService;
    }

    public async Task<InventoryTransactionDto> CreateTransactionAsync(CreateInventoryTransactionRequest request)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId);

        if (product == null)
            throw new Exception("Product not found");

        var oldStock = product.StockQuantity;

        if (request.Type == InventoryTransactionType.StockIn)
            product.StockQuantity += request.Quantity;
        else
        {
            if (product.StockQuantity < request.Quantity)
                throw new Exception("Insufficient stock");

            product.StockQuantity -= request.Quantity;
        }

        var transaction = new InventoryTransaction
        {
            Id = Guid.NewGuid(),
            ProductId = request.ProductId,
            Type = request.Type,
            Quantity = request.Quantity,
            Notes = request.Notes
        };

        _context.InventoryTransactions.Add(transaction);

        await _context.SaveChangesAsync();

        await _auditService.LogAsync(
            "SYSTEM",
            request.Type.ToString(),
            "Inventory",
            product.Id.ToString(),
            newValues: new
            {
                OldStock = oldStock,
                NewStock = product.StockQuantity,
                request.Quantity
            }
        );

        return new InventoryTransactionDto
        {
            Id = transaction.Id,
            ProductId = product.Id,
            ProductName = product.Name,
            Type = transaction.Type,
            Quantity = transaction.Quantity,
            Notes = transaction.Notes,
            CreatedAt = transaction.CreatedAt
        };
    }

    public async Task<List<InventoryTransactionDto>> GetAllAsync()
    {
        return await _context.InventoryTransactions
            .Include(x => x.Product)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new InventoryTransactionDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                Type = x.Type,
                Quantity = x.Quantity,
                Notes = x.Notes,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
    }
}