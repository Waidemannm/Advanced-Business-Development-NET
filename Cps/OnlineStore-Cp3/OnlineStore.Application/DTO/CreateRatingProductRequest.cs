using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Enum;

namespace OnlineStore.Application.DTO;

/// <summary>
/// DTO para criação de uma nova avaliação de produto.
/// Recebido no body das requisições POST /api/ratingproduct.
/// </summary>
public record CreateRatingProductRequest(
    Guid IdProduct,
    Guid IdCostumer,
    ScoreEnum? Score)
{
    /// <summary>Converte o DTO para a entidade de domínio RatingProduct.</summary>
    public RatingProduct ToDomain() => new(IdProduct, IdCostumer, Score);
}
