using MedicalERP.Application.DTOs.Sales;

namespace MedicalERP.Application.Interfaces;

public interface ISaleService
{
    Task<SaleDto> CreateSaleAsync(CreateSaleRequest request);

    Task<List<SaleDto>> GetAllAsync();

    Task<SaleDto?> GetByIdAsync(Guid id);
}