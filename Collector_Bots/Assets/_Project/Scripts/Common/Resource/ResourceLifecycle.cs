using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ResourceLifecycle : MonoBehaviour
{
    private const int ACTIVE_RESOURCE_COUNT = 4;
    private const int POINT_FOR_COLLECT = 1;
    
    [SerializeField] private List<Transform> _resourcePoint;
    [SerializeField] private float _timePerSpawn;

    private Random _random;
    private ObjectPool<Resource> _pool;
    private List<Resource> _activeResources;
    private ScoreController _scoreController;
    private float _currentTime;
    
    public void Init(Random random, ObjectPool<Resource> res, ScoreController scoreController)
    {
        _random = random;
        _pool = res;
        _scoreController = scoreController;
        _activeResources = new List<Resource>();
    }

    private void Update()
    {
        TimeSpawnResource();
    }

    public void ReturnResourceToPool(Resource resource)
    {
        if (_activeResources.Remove(resource))
        {
            _pool.ReleaseItem(resource);
            _scoreController.AddPoints(POINT_FOR_COLLECT);
        }
    }

    private Transform GetRandomPosition()
    {
        int randomValue = _random.Next(0, _resourcePoint.Count);
        return _resourcePoint[randomValue];
    }

    private void SpawnResource(Transform position)
    {
        Resource resource = _pool.GetItem();
        resource.transform.position = position.transform.position;
        _activeResources.Add(resource);
    }

    private void TimeSpawnResource()
    {
        _currentTime += Time.deltaTime;
        if (_activeResources.Count <= ACTIVE_RESOURCE_COUNT)
        {
            if (_currentTime >= _timePerSpawn)
            {
                SpawnResource(GetRandomPosition());
                _currentTime = 0f;
            }
        }
    }
    
    public IReadOnlyList<Resource> GetActiveResources() => _activeResources;
}