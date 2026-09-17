using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Enum;

namespace OnlineStore.Application.DTO;

/// <summary>
/// DTO para criação de um novo cliente.
/// Recebido no body das requisições POST /api/costumer.
/// </summary>
public record CreateCostumerRequest(
    Guid IdPayment,
    Guid IdAddress,
    /// <example>João Silva</example>
    string Name,
    /// <example>1990-05-20</example>
    DateOnly BirthDate,
    GenderEnum? Gender,
    /// <example>joao.silva@email.com</example>
    string Email)
{
    /// <summary>Converte o DTO para a entidade de domínio Costumer.</summary>
    public Costumer ToDomain() => new(IdPayment, IdAddress, Name, BirthDate, Gender, Email);
}
