using Newtonsoft.Json;
using System.Net;
using ToDoList.Persistence.Exceptions;

namespace ToDoList.Endpoint;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Serilog.ILogger _logger;

    public ExceptionMiddleware(RequestDelegate next, Serilog.ILogger logger)
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
        catch (NotFoundException ex)
        {
            _logger.Error(ex, "An unexpected error occurred");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            var response = new { ex.Message };
            var jsonResponse = JsonConvert.SerializeObject(response);

            await context.Response.WriteAsync(jsonResponse);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "An unexpected error occurred");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new { Message = "An unexpected error occurred. Please try again later." };
            var jsonResponse = JsonConvert.SerializeObject(response);

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}