namespace Task11;

public class Strongman : Solider
{
    private const double ATTACK_MULTIPLIER = 1.6;

    public Strongman(double helthPoints, double damage, double protection) : base(helthPoints, damage, protection)
    {
    }

    public override void AttackEnemy(List<Solider> enemySoldiers)
    {
        if (enemySoldiers.Count == 0) return;
        
        enemySoldiers[0].TakeDamage(Damage * ATTACK_MULTIPLIER);
    }

    public override void TakeDamage(double value)
    {
        double effectiveDamage = Math.Max(0, value - Protection);
        HelthPoints -= effectiveDamage;
    }
}