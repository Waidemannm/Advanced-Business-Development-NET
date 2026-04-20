using OnlineSore.Domain.Entities;

namespace OnlineStore.Application.DTO;

/// <summary>
/// DTO para criação de novo endereço.
/// Recebido no body das requisições POST /api/addresses.
/// </summary>
/// <param name="street">Nome rua.</param>
/// <param name="City">Nome cidade.</param>
/// <param name="state">Nome estado.</param>
/// <param name="postalCode">Código Postal.</param>
/// <param name="number">Número.</param>
/// <param name="country">Nome país.</param>
public record CreateAddressRequest(string street, string city, string state, string postalCode, string number, string country){   
    /// <summary>Converte o DTO para a entidade de domínio Address.</summary>
    public Address ToDomain(){
        return new Address(street, city, state, postalCode, number, country);
    }
}