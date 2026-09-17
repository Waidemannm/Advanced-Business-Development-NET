using OnlineStore.Domain.Common;

namespace OnlineStore.Domain.Entities;

public class Address : BaseEntity
{
    public string Street { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string PostalCode { get; private set; } = string.Empty;
    public string Number { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;

    public Address() { }

    public Address(string street, string city, string state, string postalCode, string number, string country)
    {
        UpdateStreet(street);
        UpdateCity(city);
        UpdateState(state);
        UpdatePostalCode(postalCode);
        UpdateNumber(number);
        UpdateCountry(country);
    }

    public void UpdateStreet(string newStreet)
    {
        if (!string.IsNullOrWhiteSpace(newStreet) && newStreet.Length <= 150)
            Street = newStreet;
        else
            throw new ArgumentException("Rua inválida.");
    }

    public void UpdateCity(string newCity)
    {
        if (!string.IsNullOrWhiteSpace(newCity) && newCity.Length <= 70)
            City = newCity;
        else
            throw new ArgumentException("Cidade inválida.");
    }

    public void UpdateState(string newState)
    {
        if (!string.IsNullOrWhiteSpace(newState) && newState.Length <= 70)
            State = newState;
        else
            throw new ArgumentException("Estado inválido.");
    }

    public void UpdatePostalCode(string newPostalCode)
    {
        if (!string.IsNullOrWhiteSpace(newPostalCode) && newPostalCode.Length <= 20)
            PostalCode = newPostalCode;
        else
            throw new ArgumentException("Código postal inválido.");
    }

    public void UpdateNumber(string newNumber)
    {
        if (!string.IsNullOrWhiteSpace(newNumber) && newNumber.Length <= 20 && newNumber.All(char.IsDigit))
            Number = newNumber;
        else
            throw new ArgumentException("Número inválido.");
    }

    public void UpdateCountry(string newCountry)
    {
        if (!string.IsNullOrWhiteSpace(newCountry) && newCountry.Length <= 30)
            Country = newCountry;
        else
            throw new ArgumentException("País inválido.");
    }
}
