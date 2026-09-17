using OnlineStore.Domain.Common;
using OnlineStore.Domain.Enum;

namespace OnlineStore.Domain.Entities;

public class Costumer : BaseEntity
{
    public Guid IdPayment { get; private set; }
    public Guid IdAddress { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateOnly BirthDate { get; private set; }
    public GenderEnum? Gender { get; private set; }
    public string Email { get; private set; } = string.Empty;

    public Costumer() { }

    public Costumer(Guid idPayment, Guid idAddress, string name, DateOnly birthDate, GenderEnum? gender, string email)
    {
        IdPayment = idPayment;
        IdAddress = idAddress;
        UpdateName(name);
        UpdateEmail(email);
        Gender = gender;
        UpdateBirthDate(birthDate);
    }

    public void UpdateName(string newName)
    {
        if (!string.IsNullOrWhiteSpace(newName) && newName.Length <= 300)
            Name = newName;
        else
            throw new ArgumentException("Nome inválido.");
    }

    public void UpdateBirthDate(DateOnly newDate)
    {
        var age = CalculateAge(newDate);
        if (age < 13)
            throw new ArgumentException("Cliente deve ter pelo menos 13 anos.");
        BirthDate = newDate;
    }

    public void UpdateEmail(string newEmail)
    {
        if (!string.IsNullOrWhiteSpace(newEmail) && newEmail.Length <= 100)
            Email = newEmail;
        else
            throw new ArgumentException("Email inválido.");
    }

    private static int CalculateAge(DateOnly date)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - date.Year;
        if (date > today.AddYears(-age)) age--;
        return age;
    }
}
