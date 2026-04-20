using OnlineSore.Domain.Entities;
using OnlineSore.Domain.Enum;

namespace OnlineStore.Application.DTO;

public record PaymentResponse(Guid Id, DateTime CreatedAt, decimal Value, PaymentEnum? PaymentWay)
{
    public static PaymentResponse FromDomain(Payment payment) => new(
        payment.Id,
        payment.CreatedAt,
        payment.Value,
        payment.PaymentWay
    );
}