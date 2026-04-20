using OnlineSore.Domain.Entities;

namespace OnlineStore.Application.DTO;

/// <summary>
/// DTO de resposta para dados da categoria.
/// Usado nas respostas da API (não expõe description).
/// </summary>
/// <param name="Id">Identificador único.</param>
/// <param name="Name">Nome categoria.</param>
public record CategoryResponse(Guid Id, DateTime CreatedAt, string Name)
{
    /// <summary>Converte a entidade Category para o DTO de resposta.</summary>
    public static CategoryResponse FromDomain(Category category) => new(
        category.Id,
        category.CreatedAt,
        category.Name
    );
}