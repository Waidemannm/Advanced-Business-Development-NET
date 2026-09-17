using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Common;

namespace OnlineStore.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementação genérica do repositório usando Entity Framework Core.
/// Utiliza <see cref="DbContext"/> e <see cref="DbSet{T}"/> para operações CRUD.
/// Registrado na DI como: <c>services.AddScoped(typeof(IRepository&lt;&gt;), typeof(Repository&lt;&gt;))</c>.
/// </summary>
/// <typeparam name="T">Tipo da entidade que herda de <see cref="BaseEntity"/>.</typeparam>
public class Repository<T>(OnlineStoreContext context) : IRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    /// <inheritdoc />
    public async Task<IReadOnlyList<T>> GetAllAsync() =>
        await _dbSet.AsNoTracking().ToListAsync();

    /// <inheritdoc />
    public async Task<T?> GetByIdAsync(Guid id) =>
        await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

    /// <inheritdoc />
    public async Task AddAsync(T entity) =>
        await _dbSet.AddAsync(entity);

    /// <inheritdoc />
    public void Delete(T entity) =>
        _dbSet.Remove(entity);

    /// <inheritdoc />
    public async Task<bool> ExistsByIdAsync(Guid id) =>
        await _dbSet.AnyAsync(e => e.Id == id);

    /// <inheritdoc />
    public async Task SaveChangesAsync() =>
        await context.SaveChangesAsync();
}
