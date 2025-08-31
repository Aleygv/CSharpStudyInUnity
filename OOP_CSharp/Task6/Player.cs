namespace Task6;

public class Player
{
    private float _hp;
    private int _coins;
    public List<Product> _items;

    public Player(float hp, int coins)
    {
        _hp = hp;
        _coins = coins;
        _items = new List<Product>();
    }

    public void ShowItems()
    {
        foreach (Product product in _items)
        {
            Console.WriteLine(product.ShowProductInfo());
        }
    }
}