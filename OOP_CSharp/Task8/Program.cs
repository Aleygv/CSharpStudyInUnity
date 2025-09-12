using Task8;

public class Programm
{
    public static void Main(string[] args)
    {
        bool _isWorking = true;

        Dictionary<ComponentType, int> components = new Dictionary<ComponentType, int>
        {
            { ComponentType.Brakes, 10 },
            { ComponentType.FuelFilter, 5 },
            { ComponentType.SparkPlugs, 8 }
        };

        Warehouse warehouse = new Warehouse(components);
        
        CarService carService = new CarService(1000, warehouse);

        while (_isWorking)
        {
            CarProblem problem = new CarProblem();
            carService.EvaluateCar(problem);
            Console.WriteLine();
            
            Console.WriteLine($"Выберете действие:\n" +
                              $"[1] Посмотреть склад\n" +
                              $"[2] Починить автомобиль\n" +
                              $"[3] Выход");
            string choice1 = Console.ReadLine();
            if (choice1 == "1")
            {
                carService.Warehouse.ShowComponents();
            }

            if (choice1 == "2")
            {
                carService.FixCar();
            }

            if (choice1 == "3")
            {
                _isWorking = false;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}