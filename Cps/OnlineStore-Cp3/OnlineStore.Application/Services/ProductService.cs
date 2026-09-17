using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;

namespace OnlineStore.Application.Services;

public class ProductService(IRepository<Product> repository) : IProductService
{
    public async Task<Product> CreateProductAsync(Product product)
    {
        await repository.AddAsync(product);
        await repository.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> GetByIdAsync(Guid id) =>
        await repository.GetByIdAsync(id);

    public async Task<Product?> GetProductByNameAsync(string name)
    {
        var all = await repository.GetAllAsync();
        return all.FirstOrDefault(p => p.Name == name);
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync() =>
        await repository.GetAllAsync();

    public async Task<Product> UpdateProductAsync(Guid id, string? name, string? description, decimal? price, int? stock)
    {
        var product = await repository.GetByIdAsync(id)
            ?? throw new ResourceNotFoundException("Produto", id);

        if (name is not null) product.UpdateName(name);
        if (description is not null) product.UpdateDescription(description);
        if (price is not null) product.UpdatePrice(price.Value);
        if (stock is not null) product.UpdateStock(stock.Value);

        await repository.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await repository.GetByIdAsync(id)
            ?? throw new ResourceNotFoundException("Produto", id);
        repository.Delete(product);
        await repository.SaveChangesAsync();
    }
}
