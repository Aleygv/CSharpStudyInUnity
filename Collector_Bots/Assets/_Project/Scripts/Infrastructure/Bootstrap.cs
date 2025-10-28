using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Resource _resourcePrefab;
    [SerializeField] private UnitFactory _factory;
    [SerializeField] private ResourceLifecycle _resourceLifecycle;
    [SerializeField] private Base _base;
    [SerializeField] private ResourceScanner _scanner;
    [SerializeField] private ScoreController _scoreController;
    
    // private List<Unit> _units;
    private Random _random;
    private ObjectPool<Resource> _resources;
    private ResourceDeliveryHandler _deliveryHandler;

    private void Awake()
    {
        _random = new Random();
        // _units = new List<Unit>();
        
        _resources = new ObjectPool<Resource>(Activate, Deactivate, Destroy, Create);
        _resources.PreWarm(5);
        
        _resourceLifecycle.Init(_random, _resources, _scoreController);
        _scanner.Init(_resourceLifecycle);
        _base.Init(_resourceLifecycle, _factory, _scanner);

        _deliveryHandler = new ResourceDeliveryHandler();
        _deliveryHandler.Init(_base, _resourceLifecycle);
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