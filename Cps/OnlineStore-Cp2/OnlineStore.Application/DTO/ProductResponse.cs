using OnlineSore.Domain.Entities;

namespace OnlineStore.Application.DTO;

public record ProductResponse(Guid Id, DateTime CreatedAt, Guid IdCategory, string Name, string Description, decimal Price, int Stock)
{
    public static ProductResponse FromDomain(Product product) => new(
        product.Id,
        product.CreatedAt,
        product.IdCategory,
        product.Name,
        product.Description,
        product.Price,
        product.Stock
    );
}