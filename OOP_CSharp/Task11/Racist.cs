namespace Task11;

public class Racist : Solider
{
    private Random _random;

    public Racist(double helthPoints, double damage, double protection, Random random) : base(helthPoints, damage,
        protection)
    {
        _random = random;
    }

    public override void AttackEnemy(List<Solider> enemySoldiers)
    {
        if (enemySoldiers.Count == 0) return;

        var aliveEnemies = enemySoldiers.Where(p => p.IsAlive()).ToList();
        if (aliveEnemies.Count == 0)
        {
            Console.WriteLine($"Живые кончились");
        }

        int targetCount = _random.Next(1, Math.Min(4, aliveEnemies.Count + 1));

        var selectedTargets = aliveEnemies.OrderBy(x => _random.Next()).Take(targetCount).ToList();

        foreach (Solider selectedTarget in selectedTargets)
        {
            selectedTarget.TakeDamage(Damage);
        }
    }

    public override void TakeDamage(double value)
    {
        double effectiveDamage = Math.Max(0, value - Protection);
        HelthPoints -= effectiveDamage;
    }
}