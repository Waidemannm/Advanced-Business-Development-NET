using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.DTO;

/// <summary>DTO de resposta para dados de produto.</summary>
public record ProductResponse(Guid Id, DateTime CreatedAt, Guid IdCategory, string Name, string Description, decimal Price, int Stock)
{
    /// <summary>Converte a entidade Product para o DTO de resposta.</summary>
    public static ProductResponse FromDomain(Product product) => new(
        product.Id, product.CreatedAt, product.IdCategory,
        product.Name, product.Description, product.Price, product.Stock);
}
