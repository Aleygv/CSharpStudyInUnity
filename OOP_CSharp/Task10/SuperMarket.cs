namespace Task10;

public class SuperMarket
{
    private Random _random;
    private int _revenue;
    public Queue<Client> Clients;
    public List<Item> AllItems;

    public SuperMarket(Queue<Client> clients, List<Item> items, Random random)
    {
        _revenue = 0;
        _random = random;
        Clients = clients;
        AllItems = items;
    }

    public void ServeClient()
    {
        Client client = Clients.Dequeue();
        ClientSelectItems(client);
        Console.WriteLine("Товары корзины клиента: ");
        client.ShowItems();
        int clientCartValue = EvaluateClientCart(client);
        if (clientCartValue > client.Money)
        {
            Console.WriteLine($"У клиента {client.Money}, а нужно {clientCartValue}");
            while (clientCartValue > client.Money)
            {
                client.DeleteItem();
                clientCartValue = EvaluateClientCart(client);
            }

            Console.WriteLine("Товары после удаления:");
            client.ShowItems();
            client.PurchaseCart(clientCartValue);
            AddRevenue(clientCartValue);
            Console.WriteLine($"Заработок магазина + {clientCartValue}$. Казна: {_revenue}$");
        }
        else
        {
            client.PurchaseCart(clientCartValue);
            AddRevenue(clientCartValue);
            Console.WriteLine($"Заработок магазина + {clientCartValue}$. Казна: {_revenue}$");
        }
    }

    private void ClientSelectItems(Client client)
    {
        for (int i = 0; i < _random.Next(5, 15); i++)
        {
            client.Cart.Add(AllItems[_random.Next(0, AllItems.Count)]);
        }
    }

    private int EvaluateClientCart(Client client)
    {
        int check = 0;
        foreach (Item item in client.Cart)
        {
            check += item.Price;
        }

        return check;
    }

    private void AddRevenue(int value)
    {
        _revenue += value;
    }
}