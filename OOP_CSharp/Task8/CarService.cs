namespace Task8;

public class CarService
{
    private const int PENALTY_VALUE = 70;
    private const int GAIN_VALUE = 120;
    
    private int _balance;
    public Warehouse Warehouse { get; }
    public CarProblem CurrentProblem { get; private set; }

    public CarService(int balance, int brakes, int fuelFilter, int sparkPlugs)
    {
        _balance = balance;
        Warehouse = new Warehouse(brakes, fuelFilter, sparkPlugs);
    }

    public void EvaluateCar(CarProblem problem)
    {
        Console.WriteLine($"Приехал новый автомобиль, проблема с {problem.ShowProblemText()}");
        CurrentProblem = problem;
    }

    public void FixCar()
    {
        if (Warehouse.UseComponent(CurrentProblem.ProblemType))
        {
            AddRepairRevenue();
            Console.WriteLine($"Автомобиль получилось починить, баланс: {_balance}");
        }
        else
        {
            PayPenalty();
            Console.WriteLine($"Нет деталей для починки, штраф 70, баланс: {_balance}");
        }
    }

    private void AddRepairRevenue()
    {
        _balance += GAIN_VALUE;
    }

    private void PayPenalty()
    {
        _balance -= PENALTY_VALUE;
    }
}