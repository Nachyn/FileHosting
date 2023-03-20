using System.Net.Mime;
using FileHosting.Shared.Api.Exceptions;
using FileHosting.Shared.AppCore.Exceptions;
using FileHosting.Shared.AppCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FileHosting.Shared.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly IAppLogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next,
        IAppLogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = StatusCodes.Status500InternalServerError;
        IDictionary<string, string[]>? result = null;

        switch (exception)
        {
            case ValidationException validationException:
                result = validationException.Errors;
                code = StatusCodes.Status400BadRequest;
                break;

            case DomainException domainException:
                result = new Dictionary<string, string[]>
                {
                    {domainException.Error.Code, new[] {domainException.Error.Message}}
                };
                code = StatusCodes.Status400BadRequest;
                break;

            default:
                _logger.LogError(exception.Message, exception);
                break;
        }

        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = code;

        if (result == null || result.Count < 1)
        {
            return Task.CompletedTask;
        }

        return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
    }
}