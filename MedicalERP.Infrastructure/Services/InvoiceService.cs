using MedicalERP.Application.DTOs.Invoice;
using MedicalERP.Application.Interfaces;
using MedicalERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace MedicalERP.Infrastructure.Services;

public class InvoiceService : IInvoiceService
{
    private readonly ApplicationDbContext _context;

    public InvoiceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<byte[]> GenerateInvoicePdfAsync(Guid saleId)
    {
        var sale = await _context.Sales
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == saleId);

        if (sale == null)
            throw new Exception("Sale not found");

        var invoice = new InvoiceDto
        {
            SaleId = sale.Id,
            InvoiceNumber = sale.InvoiceNumber,
            CustomerName = sale.CustomerName,
            Date = sale.CreatedAt,
            GrandTotal = sale.TotalAmount,

            Items = sale.Items.Select(x => new InvoiceItemDto
            {
                ProductName = x.Product.Name,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice
            }).ToList()
        };

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);

                page.Header()
                    .Text("MEDICAL STORE INVOICE")
                    .FontSize(22)
                    .Bold();

                page.Content().Column(column =>
                {
                    column.Spacing(10);

                    column.Item().Text($"Invoice #: {invoice.InvoiceNumber}");
                    column.Item().Text($"Customer: {invoice.CustomerName}");
                    column.Item().Text($"Date: {invoice.Date}");

                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(4);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Product").Bold();
                            header.Cell().Text("Qty").Bold();
                            header.Cell().Text("Price").Bold();
                            header.Cell().Text("Total").Bold();
                        });

                        foreach (var item in invoice.Items)
                        {
                            table.Cell().Text(item.ProductName);
                            table.Cell().Text(item.Quantity.ToString());
                            table.Cell().Text(item.UnitPrice.ToString("0.00"));
                            table.Cell().Text(item.Total.ToString("0.00"));
                        }
                    });

                    column.Item()
                        .AlignRight()
                        .Text($"Grand Total: {invoice.GrandTotal:0.00}")
                        .FontSize(18)
                        .Bold();
                });

                page.Footer()
                    .AlignCenter()
                    .Text("Thank you for visiting!");
            });
        }).GeneratePdf();
    }
}