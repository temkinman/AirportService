using System.Text.Json;
using Airport.Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Airport.Application.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionMiddleware> _logger;

    public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }
    
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = StatusCodes.Status500InternalServerError;
        var result = exception.InnerException?.Message ?? exception.Message;
        
        switch (exception)
        {
            case ValidationException validationException:
                code = StatusCodes.Status400BadRequest;
                
                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
    
                result =  JsonSerializer.Serialize(errors);
                _logger.LogError(exception, "ValidationException was occured");
                break;
            case NotFoundException:
                code = StatusCodes.Status404NotFound;
                _logger.LogError(exception, "NotFoundException was occured");
                break;
            case TaskCanceledException:
            case OperationCanceledException:
                code = 499;
                result = JsonSerializer.Serialize(exception.Message);
                _logger.LogInformation(exception, "Operation was canceled");
                break;
            case InvalidOperationException operationException:
                result = JsonSerializer.Serialize(operationException.Message);
                _logger.LogError(exception, "InvalidOperationException was occured");
                break;
            default:
                _logger.LogError(exception, "Unexpected error occurred");
                break;
        }
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;
    
        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }
    
        return context.Response.WriteAsync(result);
    }
}