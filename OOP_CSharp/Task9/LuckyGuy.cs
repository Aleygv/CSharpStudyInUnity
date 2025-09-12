namespace Task9;

public class LuckyGuy : Warrior
{
    private const double CRITICAL_CHANCE = 0.2; 
    private Random _random;
    
    public LuckyGuy(string name, int health, int damage, int protection, Random random) : base(name, health, damage, protection)
    {
        _random = random;
    }

    public override void TakeDamage(int damage)
    {
        int effectiveDamage = Math.Max(0, damage - Protection);
        Health -= effectiveDamage;
        Console.WriteLine($"{Name} получил {effectiveDamage} урона.");
    }

    public override void DealDamage(Warrior warrior)
    {
        if (_random.NextDouble() <= CRITICAL_CHANCE)
        {
            Console.Write($"{Name} атакует {warrior}... 💥 КРИТИЧЕСКИЙ УДАР! ");
            warrior.TakeDamage(Damage * 2);
        }
        else
        {
            Console.Write($"{Name} атакует {warrior}... ");
            warrior.TakeDamage(Damage);
        }
    }
}