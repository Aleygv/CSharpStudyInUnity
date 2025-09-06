namespace Task8;

public class CarService
{
    private const int PENALTY_VALUE = 70;
    private const int GAIN_VALUE = 120;
    
    public Warehouse Warehouse { get; }
    private int _balance;
    private Random _random;
    private int _numberOfComponent;

    public CarService(int balance)
    {
        _balance = balance;
        Warehouse = new Warehouse();
        _random = new Random();
    }

    public void EvaluateCar()
    {
        Console.WriteLine($"Приехал новый автомобиль, проблема с {FindProblem(ref _numberOfComponent)}");
    }

    public void FixCar()
    {
        if (Warehouse.UseComponent((ComponentType)_numberOfComponent))
        {
            MakeMoney();
            Console.WriteLine($"Автомобиль получилось починить, баланс: {_balance}");
        }
        else
        {
            PayPenalty();
            Console.WriteLine($"Нет деталей для починки, штраф 70, баланс: {_balance}");
        }
    }

    private void MakeMoney()
    {
        _balance += GAIN_VALUE;
    }

    private void PayPenalty()
    {
        _balance -= PENALTY_VALUE;
    }

    private ComponentType FindProblem(ref int number)
    {
        number = _random.Next(0, 3);
        return (ComponentType)number;
    }
}