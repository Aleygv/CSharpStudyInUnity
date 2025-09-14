namespace Task12;

public class Aviary
{
    private List<Animal> _animals;

    public Aviary(List<Animal> animals)
    {
        _animals = animals;
    }

    public void ShowAviaryInfo()
    {
        Console.WriteLine($"Это вольер с {DefineAnimalType()}\n" +
                          $"Количество животных: {CalculateAnimals()}\n" +
                          $"Информация по животным:");
        foreach (Animal animal in _animals)
        {
            animal.ShowAnimalInfo();
            Console.WriteLine("------------------------------------");
        }
    }

    private int CalculateAnimals()
    {
        return _animals.Count;
    }

    public string? DefineAnimalType()
    {
        return Enum.GetName(_animals[0].Type);
    }
}