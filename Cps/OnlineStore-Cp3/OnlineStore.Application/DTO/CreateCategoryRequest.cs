using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.DTO;

/// <summary>
/// DTO para criação de uma nova categoria.
/// Recebido no body das requisições POST /api/category.
/// </summary>
public record CreateCategoryRequest(
    /// <example>Eletrônicos</example>
    string Name,
    /// <example>Produtos eletrônicos em geral</example>
    string Description)
{
    /// <summary>Converte o DTO para a entidade de domínio Category.</summary>
    public Category ToDomain() => new(Name, Description);
}
