using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _spawnRate = 3f;

    private DelObjectPool<Enemy> _enemyPool;
    private System.Random _random;
    private float _spawnTimer;
    private DelObjectPool<Bullet> _enemyBulletPool;

    public void Init(DelObjectPool<Enemy> pool, DelObjectPool<Bullet> bulletPool, System.Random random)
    {
        _enemyPool = pool;
        _enemyBulletPool = bulletPool;
        _random = random;
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
        enemy.Init(ReturnEnemyToPool, _enemyBulletPool);
        enemy.transform.position = spawnPoint.position;
    }

    private void ReturnEnemyToPool(Enemy enemy)
    {
        _enemyPool.ReleaseItem(enemy);
    }
}