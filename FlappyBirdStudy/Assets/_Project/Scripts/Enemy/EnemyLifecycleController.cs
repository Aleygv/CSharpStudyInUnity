using System.Collections.Generic;
using UnityEngine;

public class EnemyLifecycleController : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _spawnRate = 3f;

    private ObjectPool<Enemy> _enemyPool;
    private System.Random _random;
    private float _spawnTimer;
    private ObjectPool<Bullet> _enemyBulletPool;
    private ScoreController _scoreController;

    public void Init(ObjectPool<Enemy> pool, ObjectPool<Bullet> bulletPool, System.Random random, ScoreController scoreController)
    {
        _enemyPool = pool;
        _enemyBulletPool = bulletPool;
        _random = random;
        _scoreController = scoreController;
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _spawnRate)
        {
            SpawnEnemy();
            _spawnTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        int randomIndex = _random.Next(_spawnPoints.Count); 
        Transform spawnPoint = _spawnPoints[randomIndex];
        
        Enemy enemy = _enemyPool.GetItem();
        enemy.Init(_enemyBulletPool);
        enemy.OnDied += ReturnEnemyToPool;
        enemy.transform.position = spawnPoint.position;
    }

    private void ReturnEnemyToPool(Enemy enemy)
    {
        // Нейронка указала, что можно одписываться, возможно так и надо
        enemy.OnDied -= ReturnEnemyToPool;

        _enemyPool.ReleaseItem(enemy);
        _scoreController.AddPoints(1);
    }
}