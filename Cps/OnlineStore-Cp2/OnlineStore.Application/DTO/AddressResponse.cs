using OnlineSore.Domain.Entities;

namespace OnlineStore.Application.DTO;

public record AddressResponse(Guid Id, string street, string city, string state, string postalCode, string number, string country)
{
    public static AddressResponse FromDomain(Address address) => new(
        address.Id,
        address.Street, 
        address.City,
        address.State,
        address.PostalCode,
        address.Number,
        address.Country
    );
}