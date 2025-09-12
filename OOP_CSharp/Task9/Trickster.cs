namespace Task9;

public class Trickster : Warrior
{
    private const double DODGE_CHANCE = 0.2;
    private Random _random;
    
    public Trickster(string name, int health, int damage, int protection, Random random) : base(name, health, damage, protection)
    {
        _random = random;
    }

    public override void TakeDamage(int damage)
    {
        int effectiveDamage = Math.Max(0, damage - Protection);
        if (_random.NextDouble() > DODGE_CHANCE)
        {
            Health -= effectiveDamage;
            Console.WriteLine($"{Name} получил {effectiveDamage} урона.");
        }
        else
        {
            Console.WriteLine($"{Name} ✨ уклонился от атаки!");
        }
    }

    public override void DealDamage(Warrior warrior)
    {
        Console.Write($"{Name} атакует {warrior}... ");
        warrior.TakeDamage(Damage);
    }
}