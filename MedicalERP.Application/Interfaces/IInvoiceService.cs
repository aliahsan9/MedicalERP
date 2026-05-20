using MedicalERP.Application.DTOs.Invoice;

namespace MedicalERP.Application.Interfaces;

public interface IInvoiceService
{
    Task<byte[]> GenerateInvoicePdfAsync(Guid saleId);
}