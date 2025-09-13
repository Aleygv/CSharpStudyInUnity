namespace Task11;

public abstract class Solider
{
    public double HelthPoints { get; set; }
    public double Damage { get; }
    public double Protection { get; }

    public Solider(double helthPoints, double damage, double protection)
    {
        HelthPoints = helthPoints;
        Damage = damage;
        Protection = protection;
    }

    public abstract void AttackEnemy(List<Solider> enemySoldiers);
    public abstract void TakeDamage(double value);

    public bool IsAlive()
    {
        return HelthPoints > 0;
    }
}