using MedicalERP.Application.DTOs.Purchases;

namespace MedicalERP.Application.Interfaces;

public interface IPurchaseService
{
    Task<PurchaseDto> CreatePurchaseAsync(
        CreatePurchaseRequest request);

    Task<List<PurchaseDto>> GetAllAsync();

    Task<PurchaseDto?> GetByIdAsync(Guid id);
}