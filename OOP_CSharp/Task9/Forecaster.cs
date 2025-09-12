namespace Task9;

public class Forecaster : Warrior
{
    private int _attackCounter;
    
    public Forecaster(string name, int health, int damage, int protection) : base(name, health, damage, protection)
    {
        _attackCounter = 0;
    }

    public override void TakeDamage(int damage)
    {
        int effectiveDamage = Math.Max(0, damage - Protection);
        Health -= effectiveDamage;
        Console.WriteLine($"{Name} получил {effectiveDamage} урона.");
    }

    public override void DealDamage(Warrior warrior)
    {
        if (_attackCounter == 3)
        {
            Console.Write($"{Name} атакует {warrior}... 🔮 ПРОГНОЗ СБЫЛСЯ! ДВОЙНОЙ УДАР! ");
            warrior.TakeDamage(Damage * 2);
            _attackCounter = 0;
        }
        else
        {
            Console.Write($"{Name} атакует {warrior}... ");
            warrior.TakeDamage(Damage);
            _attackCounter++;
        }
    }
}