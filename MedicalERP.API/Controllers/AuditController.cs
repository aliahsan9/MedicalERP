using MedicalERP.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalERP.API.Controllers;

[ApiController]
[Route("api/audit")]
public class AuditController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuditController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetLogs()
    {
        var logs = await _context.AuditLogs
            .OrderByDescending(x => x.CreatedAt)
            .Take(200)
            .ToListAsync();

        return Ok(logs);
    }
}