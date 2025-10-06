using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShooter : Shooter
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private ReloadTimeView _reloadTimeView;

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
        _reloadTimeView.ChangeBarValue(FireTimer);
        FireTimer += Time.deltaTime;
    }

    private void OnShootInput()
    {
        if (FireTimer >= _fireRate)
        {
            Shoot();
            FireTimer = 0f;
        }
    }
}