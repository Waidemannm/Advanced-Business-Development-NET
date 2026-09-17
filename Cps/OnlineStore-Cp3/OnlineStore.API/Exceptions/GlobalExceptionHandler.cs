using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Exceptions;

namespace OnlineStore.API.Exceptions;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IHostEnvironment environment) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var traceId = httpContext.TraceIdentifier;

        logger.LogError(exception, "Exceção não tratada: {Message}. TraceId: {TraceId}", exception.Message, traceId);

        var (status, title) = exception switch
        {
            ResourceNotFoundException => (StatusCodes.Status404NotFound,           "Recurso não encontrado"),
            KeyNotFoundException       => (StatusCodes.Status404NotFound,           "Recurso não encontrado"),
            ArgumentNullException      => (StatusCodes.Status400BadRequest,         "Requisição inválida"),
            ArgumentException          => (StatusCodes.Status400BadRequest,         "Requisição inválida"),
            InvalidOperationException  => (StatusCodes.Status400BadRequest,         "Operação inválida"),
            DomainException            => (StatusCodes.Status400BadRequest,         "Regra de negócio violada"),
            _                          => (StatusCodes.Status500InternalServerError, "Erro interno do servidor")
        };

        var detail = environment.IsDevelopment()
            ? exception.ToString()
            : (status == StatusCodes.Status500InternalServerError
                ? "Ocorreu um erro inesperado. Tente novamente mais tarde."
                : exception.Message);

        var problem = new ProblemDetails
        {
            Status   = status,
            Title    = title,
            Detail   = detail,
            Type     = $"https://httpstatuses.com/{status}",
            Instance = httpContext.Request.Path
        };

        if (environment.IsDevelopment())
        {
            problem.Extensions["traceId"] = traceId;
        }

        httpContext.Response.StatusCode  = status;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
        return true;
    }
}
