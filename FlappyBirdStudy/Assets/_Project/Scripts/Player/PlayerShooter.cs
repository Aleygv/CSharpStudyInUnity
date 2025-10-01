using System;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private static readonly Vector2 DIRECTION = Vector2.right;

    [SerializeField] private InputService _inputService;
    [SerializeField] private Transform _bulletPlace;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _fireRate;
    [SerializeField] private UI_ReloadTime _uiReloadTime;

    private DelObjectPool<Bullet> _pool;
    private float _fireTimer;

    public void Init(DelObjectPool<Bullet> pool)
    {
        _pool = pool;
    }

    private void OnEnable()
    {
        _inputService.Shoot += OnShoot;
    }

    private void OnDisable()
    {
        _inputService.Shoot -= OnShoot;
    }

    private void Update()
    {
        _uiReloadTime.ChangeBarValue(_fireTimer);
        _fireTimer += Time.deltaTime;
    }

    private void OnShoot()
    {
        if (_fireTimer >= _fireRate)
        {
            Bullet item = _pool.GetItem();
            item.Init(_bulletSpeed, DIRECTION, ReturnBulletToPool);
            item.transform.position = _bulletPlace.position;
            _fireTimer = 0f;
        }
    }

    private void ReturnBulletToPool(Bullet bullet)
    {
        _pool.ReleaseItem(bullet);
    }
}