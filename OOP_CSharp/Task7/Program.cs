using Task7;

public class Programm
{
    public static void Main(string[] args)
    {
        Direction direction = null;
        
        while (true)
        {
            Console.WriteLine($"Выберете действие:\n" +
                              $"[1] Создать направление\n" +
                              $"[2] Продать билеты\n" +
                              $"[3] Сформировать поезд\n" +
                              $"[4] Отправить поезд");
            
            string input1 = Console.ReadLine();
            
            switch (input1)
            {
                case "1":
                    Console.Write($"Введите название направления: ");
                    string input2 = Console.ReadLine();
                    direction = new Direction(input2);
                    break;

                case "2":
                    direction.SellTickets();
                    break;

                case "3":
                    direction.FormTrain();
                    break;

                case "4":
                    direction.SendTrain();
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}