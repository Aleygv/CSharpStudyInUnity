namespace Task9;

public class Wizard : Warrior
{
    private int _manaAmount;
    private const int FIREBALL_DAMAGE = 5;
    
    public Wizard(string name, int health, int damage, int protection, int manaAmount) : base(name, health, damage, protection)
    {
        _manaAmount = manaAmount;
    }

    public override void TakeDamage(int damage)
    {
        int effectiveDamage = Math.Max(0, damage - Protection);
        Health -= effectiveDamage;
        Console.WriteLine($"{Name} получил {effectiveDamage} урона.");
    }

    public override void DealDamage(Warrior warrior)
    {
        if (_manaAmount > 0)
        {
            Console.Write($"{Name} атакует {warrior}... 🧙‍♂️ ОГНЕННЫЙ ШАР! ");
            warrior.TakeDamage(Damage + FIREBALL_DAMAGE);
            _manaAmount--;
            Console.WriteLine($"⚡ Мана: {_manaAmount}");
        }
        else
        {
            Console.Write($"{Name} атакует {warrior}... ");
            warrior.TakeDamage(Damage);
            Console.WriteLine($"⚡ Нет маны — обычный удар.");
        }
    }

    public void ShowStats()
    {
        Console.WriteLine("Боец Волшебник:\n" +
                          $"Здоровье [{Health}]\n" +
                          $"Урон [{Damage}] + урон от заклинаний [{FIREBALL_DAMAGE}]\n" +
                          $"Защита [{Protection}]\n");
    }
}