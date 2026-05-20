using MedicalERP.Application.DTOs.Product;

namespace MedicalERP.Application.Interfaces;

public interface IProductService
{
    Task<ProductDto> CreateAsync(CreateProductRequest request);
    Task<List<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(Guid id);
    Task<ProductDto> UpdateAsync(Guid id, UpdateProductRequest request);
    Task<bool> DeleteAsync(Guid id);
}