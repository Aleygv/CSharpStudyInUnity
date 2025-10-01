using System;
using UnityEngine;
using Random = System.Random;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;

    private DelObjectPool<Bullet> _playerBulletPool;
    private DelObjectPool<Bullet> _enemyBulletPool;
    private DelObjectPool<Enemy> _enemyPool;
    private Random _random;

    [SerializeField] private PlayerShooter _playerShooter;
    [SerializeField] private Bullet _playerBullet;

    [SerializeField] private Bullet _enemyBullet;

    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Enemy _enemyPrefab;

    private void Awake()
    {
        _random = new Random();
        
        _playerBulletPool = new DelObjectPool<Bullet>((bullet => bullet.gameObject.SetActive(true)),
            bullet => bullet.gameObject.SetActive(false), bullet => Destroy(bullet), () => Instantiate(_playerBullet));
        _playerBulletPool.PreWarm(5);

        _enemyBulletPool = new DelObjectPool<Bullet>((bullet => bullet.gameObject.SetActive(true)),
            bullet => bullet.gameObject.SetActive(false), bullet => Destroy(bullet), () => Instantiate(_enemyBullet));
        _enemyBulletPool.PreWarm(10);
        
        _enemyPool = new DelObjectPool<Enemy>((enemy => enemy.gameObject.SetActive(true)),
            enemy => enemy.gameObject.SetActive(false), enemy => Destroy(enemy), () => Instantiate(_enemyPrefab));
        _enemyPool.PreWarm(10);

        _player.OnPlayerDied += OnPlayerDied;
    }

    private void Start()
    {
        _playerShooter.Init(_playerBulletPool);
        _enemySpawner.Init(_enemyPool, _enemyBulletPool, _random);
    }

    private void OnPlayerDied()
    {
        Debug.Log("Player died");
    }

    private void OnDestroy()
    {
        if (_player != null)
            _player.OnPlayerDied -= OnPlayerDied;
    }
}