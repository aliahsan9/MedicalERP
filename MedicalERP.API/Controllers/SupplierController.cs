using MedicalERP.Domain.Entities;
using MedicalERP.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalERP.API.Controllers;

[ApiController]
[Route("api/suppliers")]
[Authorize(Policy = "AdminOnly")]
public class SupplierController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SupplierController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Supplier supplier)
    {
        supplier.Id = Guid.NewGuid();

        _context.Suppliers.Add(supplier);

        await _context.SaveChangesAsync();

        return Ok(supplier);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Suppliers.ToListAsync());
    }
}