using UnityEngine;

public class EnemyShooter : Shooter
{
    private void Update()
    {
        FireTimer += Time.deltaTime;
        if (FireTimer >= _fireRate)
        {
            Shoot();
        }
    }
}