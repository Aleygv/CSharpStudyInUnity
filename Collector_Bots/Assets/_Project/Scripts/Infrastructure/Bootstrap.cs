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
    [SerializeField] private BaseView _baseView;
    // [SerializeField] private FlagController _flagController;
    
    // private List<Unit> _units;
    private Random _random;
    private ObjectPool<Resource> _resources;
    private ResourceDeliveredHandler _deliveredHandler;

    private void Awake()
    {
        _random = new Random();
        // _units = new List<Unit>();
        
        _resources = new ObjectPool<Resource>(Activate, Deactivate, Destroy, Create);
        _resources.PreWarm(5);
        
        _resourceLifecycle.Init(_random, _resources, _scoreController);
        _scanner.Init(_resourceLifecycle);
        _baseView.Init();
        _base.Init(_factory, _scanner, _baseView, _scoreController);

        _deliveredHandler = new ResourceDeliveredHandler();
        _deliveredHandler.Init(_resourceLifecycle);
        _base.OnResourceDelivered += _deliveredHandler.OnResourceDeliveryHandler;
        // _flagController.Init();
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