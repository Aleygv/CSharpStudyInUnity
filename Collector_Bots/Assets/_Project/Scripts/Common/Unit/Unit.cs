using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Transform _baseTransform;
    [SerializeField] private Transform _resourceCarryingPoint;

    private Resource _targetResource;
    private IUnitState _currentState;
    private bool _isBusy = false;
    private Dictionary<Type, IUnitState> _states;

    [SerializeField] private PathNavigator _navigator;
    public PathNavigator Navigator => _navigator;
    public bool IsBusy => _isBusy;
    
    public UnitIdleState IdleState { get; private set; }
    private GetResourceState GetResourceState { get; set; }
    public ReturnToBaseState ReturnState { get; private set; }
    public event Action<Resource> OnResourceDelivered;
    
    public void Init(Dictionary<Type, IUnitState> states)
    {
        _states = states;
        IdleState = (UnitIdleState)_states[typeof(UnitIdleState)];
        GetResourceState = (GetResourceState)_states[typeof(GetResourceState)];
        ReturnState = (ReturnToBaseState)_states[typeof(ReturnToBaseState)];
        _currentState = IdleState;
    }

    public void Update()
    {
        _currentState?.Update(this);
    }

    public void SetTarget(Vector3 target)
    {
        _navigator.SetTarget(target);
    }

    public void EnterState<T>() where T : IUnitState
    {
        _currentState?.Exit(this);
        _currentState = _states[typeof(T)];
        _currentState?.Enter(this);
    }

    public void SetState(IUnitState newState)
    {
        _currentState?.Exit(this);
        _currentState = newState;
        _currentState?.Enter(this);
    }
    
    public void GetResource(Resource resource)
    {
        _targetResource = resource;
        // SetState(GetResourceState);
        EnterState<GetResourceState>();
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