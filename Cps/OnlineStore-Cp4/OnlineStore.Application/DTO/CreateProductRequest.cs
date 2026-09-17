using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.DTO;

/// <summary>
/// DTO para criação de um novo produto.
/// Recebido no body das requisições POST /api/product.
/// </summary>
public record CreateProductRequest(
    Guid IdCategory,
    /// <example>Notebook Gamer</example>
    string Name,
    /// <example>Notebook para jogos com alta performance</example>
    string Description,
    /// <example>4999.99</example>
    decimal Price,
    /// <example>10</example>
    int Stock)
{
    /// <summary>Converte o DTO para a entidade de domínio Product.</summary>
    public Product ToDomain() => new(IdCategory, Name, Description, Price, Stock);
}
