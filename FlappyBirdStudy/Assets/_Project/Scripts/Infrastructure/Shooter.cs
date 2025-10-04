using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    [SerializeField] protected Transform _bulletPlace;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected float _fireRate;
    [SerializeField] protected Vector2 DIRECTION;

    protected ObjectPool<Bullet> _pool;
    protected float _fireTimer;
    
    public abstract void Init(ObjectPool<Bullet> pool);

    protected void Shoot()
    {
        Bullet item = _pool.GetItem();
        item.Init(_bulletSpeed, DIRECTION);
        item.OnHit += ReturnBulletToPool;
        item.transform.position = _bulletPlace.position;
        _fireTimer = 0f;
    }
    
    private void ReturnBulletToPool(Bullet bullet)
    {
        _pool.ReleaseItem(bullet);
    }
}
