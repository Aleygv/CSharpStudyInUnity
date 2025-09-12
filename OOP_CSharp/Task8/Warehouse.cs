namespace Task8;

public class Warehouse
{
    private Dictionary<ComponentType, int> _components { get; }

    public Warehouse(Dictionary<ComponentType, int> components)
    {
        _components = components;
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