using OnlineSore.Domain.Enum;
 
 namespace OnlineSore.Domain.Entities;
 
 public class RatingProduct
 {
     public Guid IdCostumer { get; private set; }
 
     public Guid IdProduct { get; private set; }
     
     public ScoreEnum? Score { get; private set; }
     
 
     public RatingProduct(){
 
     }
 
     public RatingProduct(Guid idProduct, Guid idCostumer, ScoreEnum? score = ScoreEnum.Regular)
     {
         IdProduct = idProduct;
         IdCostumer =  idCostumer;
         Score = score;
     }
 }