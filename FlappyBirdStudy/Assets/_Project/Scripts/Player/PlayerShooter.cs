using System;
using UnityEngine;

public class PlayerShooter : Shooter
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private UI_ReloadTime _uiReloadTime;

    public override void Init(ObjectPool<Bullet> pool)
    {
        _pool = pool;
    }

    private void OnEnable()
    {
        _inputService.Shoot += OnShootInput;
    }

    private void OnDisable()
    {
        _inputService.Shoot -= OnShootInput;
    }

    private void Update()
    {
        _uiReloadTime.ChangeBarValue(_fireTimer);
        _fireTimer += Time.deltaTime;
    }
    
    private void OnShootInput()
    {
        if (_fireTimer >= _fireRate)
        {
            Shoot();
            _fireTimer = 0f;
        }
    }
}