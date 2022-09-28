using System.Text.Json;

public enum Sex
{
    Undefined = 0,
    SigmaMale = 1,
    Female = 2,
}

public class Customer
{
    public string Name { get; init; }
    public string Surname { get; init; }
    private int _age { get; set; }
    public int Age
    {
        get => _age; set
        {
            if (value >= 18) _age = value;
            else throw new ArgumentException("Customer's age must be at least 18");
        }
    }
    public Sex Sex { get; init; }
    public Customer(string name, string surname, int age, Sex sex)
    {
        Name = name;
        Surname = surname;
        Age = age;
        Sex = sex;
    }

    public override string ToString()
        => JsonSerializer.Serialize(this);
}