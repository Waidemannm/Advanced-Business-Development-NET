using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces;

public interface IProductService
{
    Product CreateProduct(Product product);
    Product? GetById(Guid id);
    Product? GetProductByName(string name);
    IReadOnlyList<Product> GetAll();
    Product UpdateProduct(Guid id, string? name, string? description, decimal? price, int? stock);
    void DeleteProduct(Guid id);
}
