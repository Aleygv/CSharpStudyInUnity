using Task6;

public class Programm
{
    public static void Main(string[] args)
    {
        bool _isWorking = true;
        
        Salesman salesman = new Salesman();
        Player player = new Player(1, 100); //Просто что у него ещё может быть
        
        salesman.AddProduct("Отмычка", 10);
        salesman.AddProduct("Пиво", 100);
        salesman.AddProduct("Катана", 500);
        salesman.AddProduct("Нож", 200);
        salesman.AddProduct("Сапоги", 700);
        salesman.AddProduct("Семечки", 50);
        salesman.AddProduct("Топор", 400);
        salesman.AddProduct("Книга Моя борьба", 10000);

        while (_isWorking)
        {
            Console.WriteLine($"Вам встретился продавец, выберете действие:\n[1] Посмотреть товар\n[2] Купить товар \n[3] Открыть инвентарь \n[Any key] Уйти");
            string choice1 = Console.ReadLine();
            if (choice1 == "1")
            {
                salesman.ShowProducts();
            }
            else if (choice1 == "2")
            {
                Console.Write("Укажите номер товара (цифрой): ");
                string choice2 = Console.ReadLine();
                salesman.SellProduct(Convert.ToInt32(choice2), player._items);
            }
            else if (choice1 == "3")
            {
                player.ShowItems();
            }
            else
            {
                _isWorking = false;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}