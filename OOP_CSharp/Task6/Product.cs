namespace Task6;

public class Product
{
    public string Name { get; }
    public int Price { get; }

    public Product(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public string ShowProductInfo()
    {
       return $"{Name}: {Price}$";
    }
}