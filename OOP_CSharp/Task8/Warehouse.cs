namespace Task8;

public class Warehouse
{
    private Dictionary<ComponentType, int> _components { get; }
    private Random _random;

    public Warehouse()
    {
        _random = new Random();
        _components = new Dictionary<ComponentType, int>();

        foreach (var type in Enum.GetValues<ComponentType>())
        {
            _components[type] = _random.Next(5, 12);
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
        _components.TryGetValue(type, out int count);
        return count;
    }

    public bool UseComponent(ComponentType type)
    {
        if (_components.TryGetValue(type, out int value) && value > 0)
        {
            _components[type] = value - 1;
            return true;
        }

        return false;
    }
}