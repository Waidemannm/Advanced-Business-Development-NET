using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Enum;

namespace OnlineStore.Application.DTO;

/// <summary>DTO de resposta para dados de cliente.</summary>
public record CostumerResponse(Guid Id, DateTime CreatedAt, Guid IdPayment, Guid IdAddress, string Name, DateOnly BirthDate, GenderEnum? Gender, string Email)
{
    /// <summary>Converte a entidade Costumer para o DTO de resposta.</summary>
    public static CostumerResponse FromDomain(Costumer costumer) => new(
        costumer.Id, costumer.CreatedAt, costumer.IdPayment, costumer.IdAddress,
        costumer.Name, costumer.BirthDate, costumer.Gender, costumer.Email);
}
