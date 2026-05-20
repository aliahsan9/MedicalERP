using MedicalERP.Application.DTOs.Product;
using MedicalERP.Application.Interfaces;
using MedicalERP.Domain.Entities;
using MedicalERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicalERP.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDto> CreateAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Category = request.Category,
            Price = request.Price,
            CostPrice = request.CostPrice,
            StockQuantity = request.StockQuantity,
            ExpiryDate = request.ExpiryDate,
            Barcode = request.Barcode
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return Map(product);
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        return await _context.Products
            .Select(p => Map(p))
            .ToListAsync();
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        return product == null ? null : Map(product);
    }

    public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductRequest request)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            throw new Exception("Product not found");

        product.Name = request.Name;
        product.Category = request.Category;
        product.Price = request.Price;
        product.CostPrice = request.CostPrice;
        product.StockQuantity = request.StockQuantity;
        product.ExpiryDate = request.ExpiryDate;
        product.Barcode = request.Barcode;
        product.IsActive = request.IsActive;

        await _context.SaveChangesAsync();

        return Map(product);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return true;
    }

    private static ProductDto Map(Product p)
    {
        return new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Category = p.Category,
            Price = p.Price,
            CostPrice = p.CostPrice,
            StockQuantity = p.StockQuantity,
            ExpiryDate = p.ExpiryDate,
            Barcode = p.Barcode,
            IsActive = p.IsActive
        };
    }
}