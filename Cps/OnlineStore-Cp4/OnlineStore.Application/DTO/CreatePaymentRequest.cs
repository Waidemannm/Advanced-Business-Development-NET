using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Enum;

namespace OnlineStore.Application.DTO;

/// <summary>
/// DTO para criação de um novo pagamento.
/// Recebido no body das requisições POST /api/payment.
/// </summary>
public record CreatePaymentRequest(
    /// <example>150.00</example>
    decimal Value,
    PaymentEnum? PaymentWay)
{
    /// <summary>Converte o DTO para a entidade de domínio Payment.</summary>
    public Payment ToDomain() => new(Value, PaymentWay);
}
