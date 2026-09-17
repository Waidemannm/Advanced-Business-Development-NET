using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.DTO;

/// <summary>
/// DTO para criação de um novo endereço.
/// Recebido no body das requisições POST /api/address.
/// </summary>
public record CreateAddressRequest(
    /// <example>Av. Paulista</example>
    string Street,
    /// <example>São Paulo</example>
    string City,
    /// <example>SP</example>
    string State,
    /// <example>01310-100</example>
    string PostalCode,
    /// <example>1000</example>
    string Number,
    /// <example>Brasil</example>
    string Country)
{
    /// <summary>Converte o DTO para a entidade de domínio Address.</summary>
    public Address ToDomain() => new(Street, City, State, PostalCode, Number, Country);
}
