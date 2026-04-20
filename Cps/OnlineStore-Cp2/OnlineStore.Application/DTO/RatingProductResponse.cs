using OnlineSore.Domain.Entities;
using OnlineSore.Domain.Enum;

namespace OnlineStore.Application.DTO;

public record RatingProductResponse(Guid IdCostumer, Guid IdProduct, ScoreEnum? Score, DateTime CreatedAt)
{
    public static RatingProductResponse FromDomain(RatingProduct ratingProduct) => new(
        ratingProduct.IdCostumer,
        ratingProduct.IdProduct,
        ratingProduct.Score,
        ratingProduct.CreatedAt
    );
}