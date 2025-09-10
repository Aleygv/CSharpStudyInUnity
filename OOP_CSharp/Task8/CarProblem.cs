namespace Task8;

public class CarProblem
{
    private static Random _random = new Random();
    public ComponentType ProblemType { get; private set; }

    public CarProblem()
    {
        ProblemType = GenerateRandomProblem();
    }

    private ComponentType GenerateRandomProblem()
    {
        ComponentType[] types = Enum.GetValues<ComponentType>();
        return types[_random.Next(0, types.Length)];
    }

    public string ShowProblemText()
    {
        return ProblemType.ToString();
    }
}