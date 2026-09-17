using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.DTO;

/// <summary>DTO de resposta para dados de endereço.</summary>
public record AddressResponse(Guid Id, DateTime CreatedAt, string Street, string City, string State, string PostalCode, string Number, string Country)
{
    /// <summary>Converte a entidade Address para o DTO de resposta.</summary>
    public static AddressResponse FromDomain(Address address) => new(
        address.Id, address.CreatedAt, address.Street, address.City,
        address.State, address.PostalCode, address.Number, address.Country);
}
