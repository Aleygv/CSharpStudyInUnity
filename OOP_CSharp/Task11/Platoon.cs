namespace Task11;

public class Platoon
{
    public List<Solider> Soldiers;

    public Platoon(List<Solider> soliders)
    {
        Soldiers = soliders;
    }

    public void AttackEnemyPlatoon(Platoon enemyPlatoon)
    {
        foreach (var soldier in Soldiers)
        {
            if (!soldier.IsAlive()) continue; // Мёртв — не атакует
            soldier.AttackEnemy(enemyPlatoon.Soldiers);
        }
    }

    public int CalculateAliveSoldiers()
    {
        return Soldiers.Count(s => s.IsAlive());
    }

    public bool IsPlatoonAlive()
    {
        return CalculateAliveSoldiers() > 0;
    }
}