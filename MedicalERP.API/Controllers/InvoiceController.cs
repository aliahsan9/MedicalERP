using MedicalERP.Application.Interfaces;
using MedicalERP.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalERP.API.Controllers;

[ApiController]
[Route("api/invoices")]
[Authorize]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    // ========================================
    // DOWNLOAD PDF INVOICE
    // ========================================

    [HttpGet("{saleId}/pdf")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.Doctor}")]
    public async Task<IActionResult> DownloadInvoice(Guid saleId)
    {
        var pdf = await _invoiceService.GenerateInvoicePdfAsync(saleId);

        return File(
            pdf,
            "application/pdf",
            $"Invoice-{saleId}.pdf"
        );
    }
}