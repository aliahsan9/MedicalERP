using MedicalERP.Application.DTOs.Purchases;
using MedicalERP.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalERP.API.Controllers;

[ApiController]
[Route("api/purchases")]
[Authorize(Policy = "AdminOnly")]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public PurchaseController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreatePurchaseRequest request)
    {
        var result = await _purchaseService
            .CreatePurchaseAsync(request);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _purchaseService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _purchaseService
            .GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}