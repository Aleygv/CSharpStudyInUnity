using Task10;

public class Programm
{
    public static void Main(string[] args)
    {
        bool _isWorking = true;
        
        Random random = new Random();
        
        List<Item> marketItems = new List<Item>()
        {
            new Item("Фасоль", 3),
            new Item("Кукуруза", 5),
            new Item("Смерть в нищете", 1),
            new Item("Картошка", 2),
            new Item("Масло оливковое", 10),
            new Item("Мороженое", 7),
            
        };
        
        List<Client> people = new List<Client>()
        {
            new Client(100, new List<Item>()),
            new Client(30, new List<Item>()),
            new Client(200, new List<Item>()),
            new Client(10, new List<Item>()),
            new Client(50, new List<Item>())
        };

        Queue<Client> clients = new Queue<Client>(people);
        SuperMarket superMarket = new SuperMarket(clients, marketItems, random);
        
        
        Console.WriteLine("Симуляция работы магазина");

        while (_isWorking)
        {
            Console.WriteLine("Выберете действие:\n" +
                              "[1] Обслужить клиента\n" +
                              "[2] Выход");
            string chose1 = Console.ReadLine();
            if (chose1 == "1")
            {
                if (clients.Count == 0)
                {
                    Console.WriteLine("Клиенты кончились");
                    Console.WriteLine("Нажмите Enter для продолжения...");
                    Console.ReadKey();
                }
                else
                {
                    superMarket.ServeClient();
                    Console.WriteLine("Нажмите Enter для продолжения...");
                    Console.ReadKey();
                }
            }

            if (chose1 == "2")
            {
                _isWorking = false;
            }
            Console.Clear();
        }
    }
}