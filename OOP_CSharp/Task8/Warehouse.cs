namespace Task8;

public class Warehouse
{
    public List<Component> Components { get; }
    private Random _random;

    public Warehouse()
    {
        _random = new Random();
        int count = _random.Next(20, 41);
        Components = new List<Component>(count);

        for (int i = 0; i < count; i++)
        {
            Components.Add(new Component((ComponentType)_random.Next(0, 4)));
        }
    }

    public void ShowComponents()
    {
        int brakes = CalculateOneDetail(ComponentType.Brakes);
        int filters = CalculateOneDetail(ComponentType.FuelFilter);
        int sparks = CalculateOneDetail(ComponentType.SparkPlugs);
        Console.WriteLine($"На складе имеется:\n" +
                          $"{brakes} тормозов\n" +
                          $"{filters} топливных фильтров\n" +
                          $"{sparks} свечей зажигания");
    }

    private int CalculateOneDetail(ComponentType type)
    {
        int temp = 0;
        foreach (Component component in Components)
        {
            if (component.Type == type)
            {
                temp++;
            }
        }

        return temp;
    }

    public bool UseComponent(ComponentType type)
    {
        for (int i = Components.Count - 1; i >= 0; i--)
        {
            if (Components[i].Type == type)
            {
                Components.RemoveAt(i);
                return true;
            }
        }

        return false;
    }
}