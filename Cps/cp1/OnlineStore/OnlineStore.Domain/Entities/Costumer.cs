using OnlineSore.Domain.Enum;

namespace OnlineSore.Domain.Entities;

public class Costumer
{

    //1:N
    public Guid IdPayment { get; private set; }

    //1:1
    public Guid IdAddress { get; private set; }

    public string Name { get; private set; }

    private DateOnly SetBirthDate { get; set; }
    
    public int Age => CalculateAge(SetBirthDate);
    
    public GenderEnum? Gender { get; private set; }
    
    public string Email { get; private set; }

    public Costumer()
    {
        
    }
    public Costumer(Guid idPayment,  Guid idAddress, string name, DateOnly birthDate, int age , GenderEnum? gender, string email)
    {
        IdPayment = idPayment;
        IdAddress = idAddress;
        updateName(name);
        updateEmail(email);
        UpdateBirthDate(birthDate);
    }
    
    
    public void updateName(string newName)
    {
        if (!string.IsNullOrWhiteSpace(newName) && newName.Length <= 300)
        {
            Name = newName;   
        }
        else
        {
            throw new Exception("Nome inválido.");
        }
    }
    
    public void UpdateBirthDate(DateOnly newDate)
    {
        var age = CalculateAge(newDate);
        
        if (age < 13)
            throw new Exception("Cliente deve ter pelo menos 13 anos.");

        SetBirthDate = newDate;
    }
    
    public void updateEmail(string newEmail)
    {
        if (!string.IsNullOrWhiteSpace(newEmail) && newEmail.Length <= 100)
        {
            Email = newEmail;   
        }
        else
        {
            throw new Exception("Email inválido.");
        }
    }
    
    private static int CalculateAge(DateOnly date)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - date.Year;
        if (date > today.AddYears(-age)) age--;
        return age;
    }
    
}