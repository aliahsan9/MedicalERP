using MedicalERP.Application.DTOs.Product;
using MedicalERP.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalERP.API.Controllers;

[ApiController]
[Route("api/products")]
[Authorize(Policy = "AdminOnly")] // ONLY ADMIN CAN MANAGE PRODUCTS
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        var result = await _productService.CreateAsync(request);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _productService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _productService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductRequest request)
    {
        var result = await _productService.UpdateAsync(id, request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _productService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return Ok(new { message = "Deleted successfully" });
    }
}