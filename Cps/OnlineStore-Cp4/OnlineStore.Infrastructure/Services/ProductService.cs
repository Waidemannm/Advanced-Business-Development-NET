using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;
using OnlineStore.Infrastructure.Persistence;

namespace OnlineStore.Infrastructure.Services;

public class ProductService(OnlineStoreContext context) : IProductService
{
    public Product CreateProduct(Product product)
    {
        context.Products.Add(product);
        context.SaveChanges();
        return product;
    }

    public Product? GetById(Guid id) =>
        context.Products.FirstOrDefault(p => p.Id == id);

    public Product? GetProductByName(string name) =>
        context.Products.FirstOrDefault(p => p.Name == name);

    public IReadOnlyList<Product> GetAll() =>
        context.Products.ToList();

    public Product UpdateProduct(Guid id, string? name, string? description, decimal? price, int? stock)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == id)
            ?? throw new ResourceNotFoundException("Produto", id);

        if (name is not null) product.UpdateName(name);
        if (description is not null) product.UpdateDescription(description);
        if (price is not null) product.UpdatePrice(price.Value);
        if (stock is not null) product.UpdateStock(stock.Value);

        context.SaveChanges();
        return product;
    }

    public void DeleteProduct(Guid id)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == id)
            ?? throw new ResourceNotFoundException("Produto", id);
        context.Products.Remove(product);
        context.SaveChanges();
    }
}
