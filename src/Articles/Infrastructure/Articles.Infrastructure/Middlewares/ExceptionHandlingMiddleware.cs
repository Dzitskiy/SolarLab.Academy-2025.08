using System.Net;
using Articles.AppServices.Exceptions;
using Articles.Contracts.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Articles.Infrastructure.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception e)
        {
            using (_logger.BeginScope(new Dictionary<string, object> { ["UserIp"] = context.Connection.RemoteIpAddress.ToString() }))
            {
                _logger.LogError(e, "Что-то пошло не так");
            }

            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var errorModel = MapError(exception, context);
        context.Response.StatusCode = errorModel.Item1;

        return context.Response.WriteAsync(JsonConvert.SerializeObject(errorModel.Item2));
    }

    private static (int, ErrorDto) MapError(Exception exception, HttpContext context) =>
        exception switch
        {
            NotFoundException e => (StatusCodes.Status404NotFound, new ErrorDto
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"Сущность с идентификатором {e.Id} не была найдена.",
                TraceId = context.TraceIdentifier
            }),

            _ => (StatusCodes.Status500InternalServerError, new ErrorDto
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "Что-то пошло не так.",
                TraceId = context.TraceIdentifier
            }),
        };
}