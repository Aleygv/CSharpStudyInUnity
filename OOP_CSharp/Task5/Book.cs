public class Book
{
    public string Name { get; }
    public string Creator { get; }
    public int Year { get; }

    public Book(string name, string creator, int year)
    {
        Name = name;
        Creator = creator;
        Year = year;
    }

    public void ShowBookInformation()
    {
        Console.WriteLine($"Название: {Name}\nАвтор: {Creator}\nГод: {Year}\n");
    }
}