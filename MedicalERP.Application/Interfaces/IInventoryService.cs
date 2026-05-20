using MedicalERP.Application.DTOs.Inventory;

namespace MedicalERP.Application.Interfaces;

public interface IInventoryService
{
    Task<InventoryTransactionDto> CreateTransactionAsync(
        CreateInventoryTransactionRequest request);

    Task<List<InventoryTransactionDto>> GetAllAsync();
}