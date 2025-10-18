using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Transform _baseTransform;
    [SerializeField] private Transform _resourceCarryingPoint;

    private Resource _targetResource;
    private IUnitState _currentState;
    private bool _isBusy = false;

    public bool IsBusy => _isBusy;
    [SerializeField] public PathNavigator _navigator;
    public event Action<Unit, Resource> OnResourceDelivered;
    
    public void Init()
    {
        _currentState = new UnitIdleState(this);
    }

    public void Update()
    {
        _currentState?.Update();
    }

    internal void SetTarget(Vector3 target)
    {
        _navigator.SetTarget(target);
    }

    public void SetState(IUnitState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }
    
    public void GetResource(Resource resource)
    {
        _targetResource = resource;
        SetState(new GetResourceState(this));
    }

    internal void MarkAsBusy(bool busy)
    {
        _isBusy = busy;
    }
    
    internal Resource GetTargetResource()
    {
        return _targetResource;
    }

    internal void CarryResource(Resource resource)
    {
        resource.gameObject.transform.position = _resourceCarryingPoint.position;
        //resource.transform.SetParent(_resourceCarryingPoint, worldPositionStays: true);
    }

    internal Vector3 GetBasePosition()
    {
        return _baseTransform.position;
    }
    
    internal void DeliveredResource()
    {
        OnResourceDelivered?.Invoke(this, _targetResource);
        _targetResource = null; // важный шаг! (по крайней мере так нейронка сказала)
    }
}