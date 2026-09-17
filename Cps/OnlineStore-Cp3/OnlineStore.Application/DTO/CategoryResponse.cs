using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.DTO;

/// <summary>DTO de resposta para dados de categoria.</summary>
public record CategoryResponse(Guid Id, DateTime CreatedAt, string Name, string Description)
{
    /// <summary>Converte a entidade Category para o DTO de resposta.</summary>
    public static CategoryResponse FromDomain(Category category) => new(
        category.Id, category.CreatedAt, category.Name, category.Description);
}
