using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    private static readonly Vector2 DIRECTION = Vector2.left;
    
    [SerializeField] private Transform _bulletPlace;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _fireRate;

    private DelObjectPool<Bullet> _pool;
    private float _fireTimer;

    public void Init(DelObjectPool<Bullet> pool)
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

    private void Shoot()
    {
        if (_pool == null)
        {
            Debug.LogError("EnemyShooter._pool is null!");
            return;
        }

        if (_bulletPlace == null)
        {
            Debug.LogError("EnemyShooter._bulletPlace is null!");
            return;
        }

        Debug.Log("Enemy is shooting!");

        Bullet item = _pool.GetItem();
        item.Init(_bulletSpeed, DIRECTION, ReturnBulletToPool);
        item.transform.position = _bulletPlace.position;
        _fireTimer = 0f;
    }

    private void ReturnBulletToPool(Bullet bullet)
    {
        _pool.ReleaseItem(bullet);
    }
}
