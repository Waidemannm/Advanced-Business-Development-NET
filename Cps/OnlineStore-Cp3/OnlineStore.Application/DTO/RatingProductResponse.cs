using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Enum;

namespace OnlineStore.Application.DTO;

/// <summary>DTO de resposta para dados de avaliação de produto.</summary>
public record RatingProductResponse(Guid IdCostumer, Guid IdProduct, ScoreEnum? Score, DateTime CreatedAt)
{
    /// <summary>Converte a entidade RatingProduct para o DTO de resposta.</summary>
    public static RatingProductResponse FromDomain(RatingProduct ratingProduct) => new(
        ratingProduct.IdCostumer, ratingProduct.IdProduct, ratingProduct.Score, ratingProduct.CreatedAt);
}
