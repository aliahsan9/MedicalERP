namespace MedicalERP.Domain.Entities;

public class AuditLog
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;
    // CREATE, UPDATE, DELETE, LOGIN, SALE_CREATED etc.

    public string EntityName { get; set; } = string.Empty;

    public string EntityId { get; set; } = string.Empty;

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public string? IpAddress { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}