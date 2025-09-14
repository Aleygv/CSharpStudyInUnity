namespace Task12;

public class Animal
{
    public AnimalType Type {get;}
    public string Gender {get;}
    public string Voice {get;}

    public Animal(AnimalType type, string gender, string voice)
    {
        Type = type;
        Gender = gender;
        Voice = voice;
    }

    public void ShowAnimalInfo()
    {
        string? name = Enum.GetName(Type);
        Console.WriteLine($"Тип: {name}\n" +
                          $"Пол: {Gender}\n" +
                          $"Звук: {Voice}");
    }
}


public enum AnimalType
{
    Wolf,
    Goose,
    Student,
    Tiger,
    Clown
}