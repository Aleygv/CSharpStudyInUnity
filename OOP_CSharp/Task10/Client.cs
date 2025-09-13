namespace Task10;

public class Client
{
    private List<Item> _bag;
    public int Money { get; private set; }
    public List<Item> Cart;

    public Client(int money, List<Item> cart)
    {
        Money = money;
        Cart = cart;
    }

    public void DeleteItem()
    {
        Cart.RemoveAt(Cart.Count - 1);
    }

    public void PurchaseCart(int value)
    {
        Money -= value;
    }

    public void AddPurchasedItems()
    {
        _bag = Cart;
    }

    public void ShowItems()
    {
        foreach (Item item in Cart)
        {
            Console.WriteLine($"-- {item.Name} {item.Price}$");
        }
    }
}