using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Resource _resourcePrefab;
    [SerializeField] private UnitFactory _unitFactory;
    [SerializeField] private BaseFactory _baseFactory;
    [SerializeField] private ResourceLifecycle _resourceLifecycle;
    [SerializeField] private ResourceScanner _scanner;
    [SerializeField] private ScoreController _scoreController;
  
    private Random _random;
    private ObjectPool<Resource> _resources;

    private void Awake()
    {
        _random = new Random();
        
        _resources = new ObjectPool<Resource>(Activate, Deactivate, Destroy, Create);
        _resources.PreWarm(5);
        
        _resourceLifecycle.Init(_random, _resources, _scoreController);
        _scanner.Init(_resourceLifecycle);
    }

    private void Start()
    {
        _baseFactory.FactoryMethod(new Vector3(-7.71f, 1.2f, 1.78f));
    }

    private void Activate(Resource resource)
    {
        resource.gameObject.SetActive(true);
    }

    private void Deactivate(Resource resource)
    {
        resource.gameObject.SetActive(false);
    }

    private void Destroy(Resource resource)
    {
        Destroy(resource.gameObject);
    }

    private Resource Create()
    {
        return Instantiate(_resourcePrefab);
    }
}