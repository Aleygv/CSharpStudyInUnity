namespace Task9;

public class Vampire : Warrior
{
    private const double TREATMENT_CHANCE = 2.0;
    
    private Random _random;
    private double _rageMeter;
    
    public Vampire(string name, int health, int damage, int protection, Random random) : base(name, health, damage, protection)
    {
        _random = random;
        _rageMeter = 0.0;
    }

    public override void TakeDamage(int damage)
    {
        int effectiveDamage = Math.Max(0, damage - Protection);
        Health -= effectiveDamage;
        _rageMeter += _random.NextDouble();
        Console.WriteLine($"{Name} получил {effectiveDamage} урона.");

        if (_rageMeter >= TREATMENT_CHANCE)
        {
            int healAmount = Health / 2;
            ActivateTreatment(healAmount);
            _rageMeter = 0.0;
            Console.WriteLine($"💉 {Name} активировал лечение и восстановил {healAmount} здоровья!");
        }
    }

    public override void DealDamage(Warrior warrior)
    {
        Console.Write($"{Name} атакует {warrior}... ");
        warrior.TakeDamage(Damage);
    }

    private void ActivateTreatment(int healthPoints)
    {
        int prevHealth = Health;
        Health += healthPoints;
        // Логика не меняется — просто выводим результат
        Console.WriteLine($"🩸 {Name} восстановил {healthPoints} здоровья (было {prevHealth}, стало {Health}).");
    }
}