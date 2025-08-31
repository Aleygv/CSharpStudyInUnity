public class BookStorage
{
    private List<Book> _books;

    public BookStorage()
    {
        _books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        _books.Remove(book);
    }

    public void ShowAllBooks()
    {
        foreach (Book book in _books)
        {
            book.ShowBookInformation();
        }
    }

    public void ShowBookByName(string name)
    {
        foreach (Book book in _books)
        {
            if (book.Name == name)
            {
                book.ShowBookInformation();
            }
        }
    }
    
    public void ShowBookByCreator(string creator)
    {
        foreach (Book book in _books)
        {
            if (book.Creator == creator)
            {
                book.ShowBookInformation();
            }
        }
    }
    
    public void ShowBookByYear(int year)
    {
        foreach (Book book in _books)
        {
            if (book.Year== year)
            {
                book.ShowBookInformation();
            }
        }
    }
}