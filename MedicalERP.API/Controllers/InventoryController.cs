using MedicalERP.Application.DTOs.Inventory;
using MedicalERP.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalERP.API.Controllers;

[ApiController]
[Route("api/inventory")]
[Authorize(Policy = "AdminOnly")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpPost("transaction")]
    public async Task<IActionResult> CreateTransaction(
        CreateInventoryTransactionRequest request)
    {
        var result = await _inventoryService
            .CreateTransactionAsync(request);

        return Ok(result);
    }

    [HttpGet("transactions")]
    public async Task<IActionResult> GetAllTransactions()
    {
        return Ok(await _inventoryService.GetAllAsync());
    }
}