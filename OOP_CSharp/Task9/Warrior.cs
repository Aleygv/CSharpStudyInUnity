namespace Task9;

public abstract class Warrior
{
    public string Name;
    public int Health { get; set; }
    public int Damage { get; }
    public int Protection { get; }

    public Warrior(string name, int health, int damage, int protection)
    {
        Name = name;
        Health = health;
        Damage = damage;
        Protection = protection;
    }

    public void ShowStats()
    {
        Console.WriteLine($"Боец {Name}:\n" +
                          $"Здоровье [{Health}]\n" +
                          $"Урон [{Damage}]\n" +
                          $"Защита [{Protection}]\n");
    }

    public abstract void TakeDamage(int damage);
    public abstract void DealDamage(Warrior warrior);

    public bool IsAlive()
    {
        return Health >= 0;
    }
    
    public override string ToString()
    {
        return Name;
    }
}