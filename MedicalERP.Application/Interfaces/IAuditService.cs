namespace MedicalERP.Application.Interfaces;

public interface IAuditService
{
    Task LogAsync(
        string userId,
        string action,
        string entityName,
        string entityId,
        object? oldValues = null,
        object? newValues = null,
        string? ipAddress = null
    );
}