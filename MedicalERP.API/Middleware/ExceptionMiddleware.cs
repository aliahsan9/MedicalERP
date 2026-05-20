using MedicalERP.Application.Common;
using System.Net;
using System.Text.Json;

namespace MedicalERP.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled Exception Occurred");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        HttpStatusCode status;
        string message;

        switch (exception)
        {
            case KeyNotFoundException:
                status = HttpStatusCode.NotFound;
                message = exception.Message;
                break;

            case UnauthorizedAccessException:
                status = HttpStatusCode.Unauthorized;
                message = "Unauthorized access";
                break;

            case ArgumentException:
                status = HttpStatusCode.BadRequest;
                message = exception.Message;
                break;

            default:
                status = HttpStatusCode.InternalServerError;
                message = "Something went wrong on server";
                break;
        }

        var response = new ErrorResponse
        {
            Success = false,
            Message = message,
            StatusCode = (int)status,

#if DEBUG
            Details = exception.ToString()
#else
            Details = null
#endif
        };

        var json = JsonSerializer.Serialize(response);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(json);
    }
}