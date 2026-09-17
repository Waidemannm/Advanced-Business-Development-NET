using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;
using OnlineStore.Infrastructure.Persistence;

namespace OnlineStore.Infrastructure.Services;

public class AddressService(OnlineStoreContext context) : IAddressService
{
    public Address CreateAddress(Address address)
    {
        context.Addresses.Add(address);
        context.SaveChanges();
        return address;
    }

    public Address? GetById(Guid id) =>
        context.Addresses.FirstOrDefault(a => a.Id == id);

    public IReadOnlyList<Address> GetAll() =>
        context.Addresses.ToList();

    public Address UpdateAddress(Guid id, string? street, string? city, string? state, string? postalCode, string? number, string? country)
    {
        var address = context.Addresses.FirstOrDefault(a => a.Id == id)
            ?? throw new ResourceNotFoundException("Endereço", id);

        if (street is not null) address.UpdateStreet(street);
        if (city is not null) address.UpdateCity(city);
        if (state is not null) address.UpdateState(state);
        if (postalCode is not null) address.UpdatePostalCode(postalCode);
        if (number is not null) address.UpdateNumber(number);
        if (country is not null) address.UpdateCountry(country);

        context.SaveChanges();
        return address;
    }

    public void DeleteAddress(Guid id)
    {
        var address = context.Addresses.FirstOrDefault(a => a.Id == id)
            ?? throw new ResourceNotFoundException("Endereço", id);
        context.Addresses.Remove(address);
        context.SaveChanges();
    }
}
