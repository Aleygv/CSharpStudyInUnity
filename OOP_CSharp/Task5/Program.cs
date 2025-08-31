public class Programm
{
    public static void Main(string[] args)
    {
        bool _isWorking = true;

        BookStorage bookStorage = new BookStorage();
        bookStorage.AddBook(new Book("Преступление и наказание", "Достоевский", 1866));
        bookStorage.AddBook(new Book("Игрок", "Достоевский", 1866));
        bookStorage.AddBook(new Book("Капитанская дочка", "Пушкин", 1836));
        bookStorage.AddBook(new Book("Дубровский", "Пушкин", 1841));
        bookStorage.AddBook(new Book("Моя борьба", "Гитлер", 1925));
        bookStorage.AddBook(new Book("Герой нашего времени", "Лермонтов", 1840));
        bookStorage.AddBook(new Book("Мцыри", "Лермонтов", 1840));
        bookStorage.AddBook(new Book("Демон", "Лермонтов", 1839));

        while (_isWorking)
        {
            Console.Write("Выберете действие:\n[1] Показать все книги\n[2] Показать книги по параметру\n[3] Выход\n");
            string par = Console.ReadLine();
            if (par == "1")
            {
                bookStorage.ShowAllBooks();
            }
            else if (par == "2")
            {
                Console.Write("Выберете по какому параметру искать\n[1] Название\n[2] Автор\n[3] Год\n");
                string par2 = Console.ReadLine();
                if (par2 == "1")
                {
                    Console.Write("Введите название книги: ");
                    bookStorage.ShowBookByName(Console.ReadLine());
                }

                if (par2 == "2")
                {
                    Console.Write("Введите автора книги: ");
                    bookStorage.ShowBookByCreator(Console.ReadLine());
                }

                if (par2 == "3")
                {
                    Console.Write("Введите год книги: ");
                    bookStorage.ShowBookByYear(Convert.ToInt32(Console.ReadLine()));
                }
            }
            else if (par == "3")
            {
                _isWorking = false;
            }

            Console.ReadKey();

            Console.Clear();
        }
    }
}
