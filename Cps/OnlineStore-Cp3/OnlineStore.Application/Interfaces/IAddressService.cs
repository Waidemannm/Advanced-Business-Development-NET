using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces;

public interface IAddressService
{
    Address CreateAddress(Address address);
    Address? GetById(Guid id);
    IReadOnlyList<Address> GetAll();
    Address UpdateAddress(Guid id, string? street, string? city, string? state, string? postalCode, string? number, string? country);
    void DeleteAddress(Guid id);
}
