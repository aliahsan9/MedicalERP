using System.Text.Json;
using MedicalERP.Application.Interfaces;
using MedicalERP.Domain.Entities;
using MedicalERP.Infrastructure.Data;

namespace MedicalERP.Infrastructure.Services;

public class AuditService : IAuditService
{
    private readonly ApplicationDbContext _context;

    public AuditService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task LogAsync(
        string userId,
        string action,
        string entityName,
        string entityId,
        object? oldValues = null,
        object? newValues = null,
        string? ipAddress = null)
    {
        var audit = new AuditLog
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Action = action,
            EntityName = entityName,
            EntityId = entityId,
            OldValues = oldValues != null
                ? JsonSerializer.Serialize(oldValues)
                : null,

            NewValues = newValues != null
                ? JsonSerializer.Serialize(newValues)
                : null,

            IpAddress = ipAddress,
            CreatedAt = DateTime.UtcNow
        };

        await _context.AuditLogs.AddAsync(audit);
        await _context.SaveChangesAsync();
    }
}