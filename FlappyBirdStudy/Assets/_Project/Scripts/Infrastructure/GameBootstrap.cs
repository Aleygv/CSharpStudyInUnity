using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;

    private ObjectPool<Bullet> _playerBulletPool;
    private ObjectPool<Bullet> _enemyBulletPool;
    private ObjectPool<Enemy> _enemyPool;
    private Random _random;

    [SerializeField] private PlayerShooter _playerShooter;
    [SerializeField] private Bullet _playerBullet;

    [SerializeField] private Bullet _enemyBullet;

    [FormerlySerializedAs("_enemySpawner")] [SerializeField] private EnemyLifecycleController _enemyLifecycleController;
    [SerializeField] private Enemy _enemyPrefab;

    [SerializeField] private ScoreController _scoreController;

    private void Awake()
    {
        _random = new Random();
        
        _playerBulletPool = new ObjectPool<Bullet>(Activate, Deactivate, Destroy, () => Instantiate(_playerBullet));
        _playerBulletPool.PreWarm(5);

        _enemyBulletPool = new ObjectPool<Bullet>(Activate, Deactivate, Destroy, () => Instantiate(_enemyBullet));
        _enemyBulletPool.PreWarm(10);
        
        _enemyPool = new ObjectPool<Enemy>(Activate, Deactivate, Destroy, () => Instantiate(_enemyPrefab));
        _enemyPool.PreWarm(10);

        _player.OnPlayerDied += OnPlayerDied;
    }

    private void Start()
    {
        _playerShooter.Init(_playerBulletPool);
        _enemyLifecycleController.Init(_enemyPool, _enemyBulletPool, _random, _scoreController);
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
    
    private void Activate(Component component) => component.gameObject.SetActive(true);
    private void Deactivate(Component component) => component.gameObject.SetActive(false);
    private void Destroy(Component component) => Destroy(component.gameObject);

}