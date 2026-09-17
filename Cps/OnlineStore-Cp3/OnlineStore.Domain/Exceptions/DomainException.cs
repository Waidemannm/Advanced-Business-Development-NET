namespace OnlineStore.Domain.Exceptions;

/// <summary>
/// Exceção base para violações de regras de negócio do domínio.
/// Mapeada para HTTP 400 Bad Request pelo GlobalExceptionHandler.
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}
