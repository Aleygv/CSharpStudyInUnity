namespace Task11;

public class Squaddie : Solider
{
    public Squaddie(double helthPoints, double damage, double protection) : base(helthPoints, damage, protection)
    {
    }

    public override void AttackEnemy(List<Solider> enemySoldiers)
    {
        if (enemySoldiers.Count == 0) return;
        
        enemySoldiers[0].TakeDamage(Damage);
    }

    public override void TakeDamage(double value)
    {
        double effectiveDamage = Math.Max(0, value - Protection);
        HelthPoints -= effectiveDamage;
    }
}