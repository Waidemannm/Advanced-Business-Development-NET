using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces;

public interface IProductService
{
    Task<Product> CreateProductAsync(Product product);
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product?> GetProductByNameAsync(string name);
    Task<IReadOnlyList<Product>> GetAllAsync();
    Task<Product> UpdateProductAsync(Guid id, string? name, string? description, decimal? price, int? stock);
    Task DeleteProductAsync(Guid id);
}
