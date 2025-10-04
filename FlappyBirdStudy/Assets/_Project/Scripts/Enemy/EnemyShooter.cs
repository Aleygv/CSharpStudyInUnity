using UnityEngine;

public class EnemyShooter : Shooter
{
    public override void Init(ObjectPool<Bullet> pool)
    {
        _pool = pool;
    }

    private void Update()
    {
        _fireTimer += Time.deltaTime;
        if (_fireTimer >= _fireRate)
        {
            Shoot();
        }
    }
}