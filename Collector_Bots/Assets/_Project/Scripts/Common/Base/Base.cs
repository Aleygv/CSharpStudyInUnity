using System;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    private const float TIME_PER_ASSIGN = 2.5f;

    // [SerializeField] private BaseIdleState _idleState;
    // [SerializeField] private BaseScanningState _scanningState;
    [SerializeField] private int _quantityOfUnits;

    private List<Unit> _units;
    private UnitFactory _factory;
    private ResourceLifecycle _resourceLifecycle;
    private ResourceScanner _scanner;
    private float _currentTime;

    public event Action<Resource> OnResourceDelivered; 
    
    public void Init(ResourceLifecycle resourceLifecycle, UnitFactory factory, ResourceScanner scanner)
    {
        _resourceLifecycle = resourceLifecycle;
        _factory = factory;
        _scanner = scanner;
        _units = new List<Unit>();
    }

    private void Start()
    {
        for (int i = 0; i < _quantityOfUnits; i++)
        {
            var unit = _factory.FactoryMethod();
            _units.Add(unit);
            unit.OnResourceDelivered += HandleResourceDelivered;
        }
    }

    private void Update()
    {
        OrganizeAppearance();
    }

    private void OrganizeAppearance()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= TIME_PER_ASSIGN)
        {
            AssignTask();
            _currentTime = 0f;
        }
    }

    private void AssignTask()
    {
        foreach (Unit unit in _units)
        {
            Resource nearest = _scanner.FindNearest(unit.transform.position);
            if (nearest != null && !unit.IsBusy)
            {
                nearest.IsReserved = true; // ← бронируем
                unit.GetResource(nearest);
            }
        }
    }

    private void HandleResourceDelivered(Resource resource)
    {
        OnResourceDelivered?.Invoke(resource);
        AssignTask();
    }
}