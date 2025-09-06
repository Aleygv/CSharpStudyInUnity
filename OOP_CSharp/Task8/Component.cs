namespace Task8;

public class Component
{
    public ComponentType Type { get; }

    public Component(ComponentType type)
    {
        Type = type;
    }
}

public enum ComponentType
{
    Brakes,
    FuelFilter,
    SparkPlugs
}