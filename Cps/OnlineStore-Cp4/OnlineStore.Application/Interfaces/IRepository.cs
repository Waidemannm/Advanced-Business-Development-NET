using OnlineStore.Domain.Common;

namespace OnlineStore.Application.Interfaces;

/// <summary>
/// Contrato genérico de repositório para entidades que herdam de <see cref="BaseEntity"/>.
/// Registrado na DI como open generic: <c>AddScoped(typeof(IRepository&lt;&gt;), typeof(Repository&lt;&gt;))</c>.
/// </summary>
/// <typeparam name="T">Tipo da entidade de domínio.</typeparam>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>Retorna todos os registros sem rastreamento (AsNoTracking).</summary>
    Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>Busca um registro pelo identificador único.</summary>
    Task<T?> GetByIdAsync(Guid id);

    /// <summary>Adiciona um novo registro ao contexto (pendente até SaveChangesAsync).</summary>
    Task AddAsync(T entity);

    /// <summary>Remove um registro do contexto (pendente até SaveChangesAsync).</summary>
    void Delete(T entity);

    /// <summary>Verifica se existe um registro com o id informado.</summary>
    Task<bool> ExistsByIdAsync(Guid id);

    /// <summary>Persiste todas as alterações pendentes no banco de dados.</summary>
    Task SaveChangesAsync();
}
