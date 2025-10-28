using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Transform _baseTransform;
    [SerializeField] private Transform _resourceCarryingPoint;

    private Resource _targetResource;
    private IUnitState _currentState;
    private bool _isBusy = false;

    [SerializeField] private PathNavigator _navigator;
    public PathNavigator Navigator => _navigator;
    public bool IsBusy => _isBusy;
    
    public IUnitState IdleState { get; private set; }
    private IUnitState GetResourceState { get; set; }
    public IUnitState ReturnState { get; private set; }
    public event Action<Resource> OnResourceDelivered;
    
    public void Init(IUnitState idleState, IUnitState getResourceState, IUnitState returnState)
    {
        IdleState = idleState;
        GetResourceState = getResourceState;
        ReturnState = returnState;
        _currentState = IdleState;
    }

    public void Update()
    {
        _currentState?.Update();
    }

    public void SetTarget(Vector3 target)
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
        SetState(GetResourceState);
    }

    public void MarkAsBusy(bool busy)
    {
        _isBusy = busy;
    }
    
    public Resource GetTargetResource()
    {
        return _targetResource;
    }

    public void CarryResource(Resource resource)
    {
        resource.gameObject.transform.position = _resourceCarryingPoint.position;
        //resource.transform.SetParent(_resourceCarryingPoint, worldPositionStays: true);
    }

    public Vector3 GetBasePosition()
    {
        return _baseTransform.position;
    }
    
    public void DeliveredResource()
    {
        OnResourceDelivered?.Invoke(_targetResource);
        _targetResource = null; // важный шаг! (по крайней мере так нейронка сказала)
    }
}