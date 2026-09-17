using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Enum;

namespace OnlineStore.Application.DTO;

/// <summary>DTO de resposta para dados de pagamento.</summary>
public record PaymentResponse(Guid Id, DateTime CreatedAt, decimal Value, PaymentEnum? PaymentWay)
{
    /// <summary>Converte a entidade Payment para o DTO de resposta.</summary>
    public static PaymentResponse FromDomain(Payment payment) => new(
        payment.Id, payment.CreatedAt, payment.Value, payment.PaymentWay);
}
