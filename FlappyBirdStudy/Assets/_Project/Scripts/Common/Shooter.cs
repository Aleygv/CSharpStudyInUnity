using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    [SerializeField] protected Transform _bulletPlace;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected float _fireRate;
    [SerializeField] protected Vector2 DIRECTION;

    protected ObjectPool<Bullet> Pool;
    protected float FireTimer;

    public void Init(ObjectPool<Bullet> pool)
    {
        Pool = pool;
    }

    protected void Shoot()
    {
        Bullet item = Pool.GetItem();
        item.Init(_bulletSpeed, DIRECTION);
        item.OnHit += ReturnBulletToPool;
        item.transform.position = _bulletPlace.position;
        FireTimer = 0f;
    }
    
    private void ReturnBulletToPool(Bullet bullet)
    {
        Pool.ReleaseItem(bullet);
    }
}
