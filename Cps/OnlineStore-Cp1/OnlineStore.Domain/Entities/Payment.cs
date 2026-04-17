using OnlineSore.Domain.Enum;

namespace OnlineSore.Domain.Entities;

public class Payment
{
    public decimal Value { get; private set; }

    public PaymentEnum? PaymentWay { get; private set; }
    
    public Payment()
    {
        
    }
    public Payment(decimal value, PaymentEnum? paymentWay = PaymentEnum.Other)
    {
        PaymentWay = paymentWay;
        UpdateValue(value);
    }

    public void UpdateValue(decimal newValue)
    {
        if (newValue > 0 && newValue < 999999999999.99m)
        {
            Value = newValue; 
        }else{
            throw new ArgumentException("O valor está inválido.");
            
        }
    }   
} 