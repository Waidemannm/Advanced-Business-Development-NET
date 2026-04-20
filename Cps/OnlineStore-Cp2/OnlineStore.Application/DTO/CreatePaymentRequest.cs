using OnlineSore.Domain.Entities;
using OnlineSore.Domain.Enum;

namespace OnlineStore.Application.DTO;

/// <summary>
/// DTO para criação de nova Forma de Pagamento.
/// Recebido no body das requisições POST /api/payments.
/// </summary>
/// <param name="Value">Valor do pagamento.</param>
/// <param name="PaymentWay">Forma de pagamento.</param>
public record CreatePaymentRequest(decimal Value, PaymentEnum? PaymentWay){
    
    /// <summary>Converte o DTO para a entidade de domínio Payment.</summary>
    public Payment ToDomain()
    {
        return new Payment(Value, PaymentWay);
    }
}