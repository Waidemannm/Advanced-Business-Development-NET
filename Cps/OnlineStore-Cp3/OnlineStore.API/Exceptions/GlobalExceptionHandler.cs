using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Exceptions;

namespace OnlineStore.API.Exceptions;

/// <summary>
/// Handler centralizado de exceções. Converte exceções em respostas RFC 7807 (ProblemDetails).
/// Registrado no pipeline via <c>app.UseExceptionHandler()</c>.
/// </summary>
/// <remarks>
/// Mapeamento de exceções para códigos HTTP:
/// <list type="table">
///   <listheader><term>Exceção</term><description>Status HTTP</description></listheader>
///   <item><term>ArgumentException / ArgumentNullException</term><description>400 Bad Request</description></item>
///   <item><term>InvalidOperationException</term><description>400 Bad Request</description></item>
///   <item><term>DomainException</term><description>400 Bad Request</description></item>
///   <item><term>ResourceNotFoundException / KeyNotFoundException</term><description>404 Not Found</description></item>
///   <item><term>Qualquer outra exceção</term><description>500 Internal Server Error</description></item>
/// </list>
/// </remarks>
public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IHostEnvironment environment) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Exceção não tratada: {Message}", exception.Message);

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

        // Em produção, detalhes internos não são expostos para evitar vazamento de informações sensíveis
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

        httpContext.Response.StatusCode  = status;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
        return true;
    }
}
