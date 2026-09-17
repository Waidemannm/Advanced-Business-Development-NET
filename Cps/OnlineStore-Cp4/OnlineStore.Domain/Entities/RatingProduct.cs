using OnlineStore.Domain.Enum;

namespace OnlineStore.Domain.Entities;

public class RatingProduct
{
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public Guid IdCostumer { get; private set; }
    public Guid IdProduct { get; private set; }
    public ScoreEnum? Score { get; private set; }

    public RatingProduct() { }

    public RatingProduct(Guid idProduct, Guid idCostumer, ScoreEnum? score = ScoreEnum.Regular)
    {
        IdProduct = idProduct;
        IdCostumer = idCostumer;
        Score = score;
    }

    public void UpdateScore(ScoreEnum score)
    {
        if (!System.Enum.IsDefined(typeof(ScoreEnum), score))
            throw new ArgumentOutOfRangeException(nameof(score), "Avaliação inválida.");
        Score = score;
    }
}
