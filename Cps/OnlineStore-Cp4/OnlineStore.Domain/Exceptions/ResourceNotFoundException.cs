namespace OnlineStore.Domain.Exceptions;

/// <summary>
/// Exceção lançada quando um recurso não é encontrado no sistema.
/// Mapeada para HTTP 404 Not Found pelo GlobalExceptionHandler.
/// </summary>
public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string resourceName, Guid id)
        : base($"{resourceName} com id '{id}' não encontrado.") { }

    public ResourceNotFoundException(string message) : base(message) { }
}
