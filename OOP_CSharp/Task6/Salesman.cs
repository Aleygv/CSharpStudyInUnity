namespace Task6;

public class Salesman
{
    private List<Product> _products;

    public Salesman()
    {
        _products = new List<Product>();
    }

    public void AddProduct(string name, int price)
    {
        _products.Add(new Product(name, price));
    }

    public void ShowProducts()
    {
        // foreach (Product product in _products)
        // {
        //     // product.ShowProductInfo();
        //     Console.WriteLine();
        // }
        for (int i = 0; i < _products.Count; i++)
        {
            Console.WriteLine($"[{i+1}] {_products[i].ShowProductInfo()}");
        }
    }

    public void SellProduct(int index, List<Product> playerItems)
    {
        index -= 1; //Для ввода с 1, а не 0
        try
        {
            if (_products.Contains(_products[index]))
            {
                Console.WriteLine($"Куплен предмет {_products[index].Name}");
                playerItems.Add(_products[index]);
                _products.Remove(_products[index]);
            }
            else
            {
                Console.WriteLine();
            }
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine($"Такого продукта нет в наличии: [{e.Message}]");
        }
    }
}