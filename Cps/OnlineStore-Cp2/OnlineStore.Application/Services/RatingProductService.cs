using OnlineInfrastructure.Persistence;
using OnlineSore.Domain.Entities;
using OnlineSore.Domain.Enum;
using OnlineStore.Application.Interfaces;

namespace OnlineStore.Application.Services;

/// <summary>
/// Implementação de avaliação de produto.
/// Utiliza armazenamento em memória (lista estática) para persistência.
/// </summary>
public class RatingProductService : IRatingProductService
{
    
    private readonly OnlineStoreContext _onlineStoreContext;
    
    public RatingProductService(OnlineStoreContext onlineStoreContext)
    {
        _onlineStoreContext = onlineStoreContext;
    }

    /// <inheritdoc />
    public RatingProduct CreateRatingProduct(RatingProduct ratingProduct)
    {
        if (ScoreExists(ratingProduct.IdCostumer, ratingProduct.IdProduct))
        {
            throw new InvalidOperationException("Este cliente já avaliou este produto.");
        }
        _onlineStoreContext.RatingProducts.Add(ratingProduct);
        return ratingProduct;
    }
    
    /// <inheritdoc />
    public RatingProduct? GetById(Guid idCostumer, Guid idProduct)
    {
        return _onlineStoreContext.RatingProducts.FirstOrDefault(r => r.IdCostumer == idCostumer && r.IdProduct == idProduct);
    }
    
    /// <inheritdoc />
    public RatingProduct? GetRatingProductByScore(ScoreEnum score)
    {
        return _onlineStoreContext.RatingProducts.FirstOrDefault(r => r.Score == score);
    }
    
    /// <inheritdoc />
    public IReadOnlyList<RatingProduct> GetAll()
    {
        return _onlineStoreContext.RatingProducts.ToList();
    }
    
    /// <inheritdoc />
    public bool ScoreExists(Guid idCostumer, Guid idProduct)
    {
        return _onlineStoreContext.RatingProducts
            .Any(rp => rp.IdCostumer == idCostumer && rp.IdProduct == idProduct);
    }
    
    /// <inheritdoc />
    public void DeleteRatingProduct(Guid idCostumer, Guid idProduct)
    {
        var ratingProduct = _onlineStoreContext.RatingProducts
                                .FirstOrDefault(rp => rp.IdCostumer == idCostumer && rp.IdProduct == idProduct)
                            ?? throw new InvalidOperationException("Avaliação não encontrada.");
        _onlineStoreContext.RatingProducts.Remove(ratingProduct);
    }
}