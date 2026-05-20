namespace MedicalERP.Domain.Entities;

public class Supplier
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;
}