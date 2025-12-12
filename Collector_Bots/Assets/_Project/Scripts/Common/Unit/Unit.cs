using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Transform _resourceCarryingPoint;
    [SerializeField] private PathNavigator _navigator;

    private Vector3 _basePosition;
    private Resource _targetResource;
    private IUnitState _currentState;
    private bool _isBusy = false;
    private Dictionary<Type, IUnitState> _states;

    public PathNavigator Navigator => _navigator;
    public bool IsBusy => _isBusy;
    public event Action<Resource> OnResourceDelivered;
    
    public void Init()
    {
        _states = new Dictionary<Type, IUnitState>();
        _states.Add(typeof(UnitIdleState), new UnitIdleState(this));
        _states.Add(typeof(GetResourceState), new GetResourceState(this));
        _states.Add(typeof(ReturnToBaseState), new ReturnToBaseState(this));
        _currentState = _states[typeof(UnitIdleState)];
    }

    public void Update()
    {
        _currentState?.Update();
    }

    public void SetTarget(Vector3 target)
    {
        _navigator.SetTarget(target);
    }

    public void EnterState<T>() where T : IUnitState
    {
        _currentState?.Exit();
        _currentState = _states[typeof(T)];
        _currentState?.Enter();
    }

    public void EnterBuildState(Base originBase, Vector3 buildPosition)
    {
        _currentState?.Exit();
        _currentState = new UnitBuildBaseState(this, originBase, buildPosition);
        _currentState?.Enter();
    }

    // public void SetState(IUnitState newState)
    // {
    //     _currentState?.Exit();
    //     _currentState = newState;
    //     _currentState?.Enter();
    // }
    
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

    public void SetBasePosition(Vector3 position)
    {
        _basePosition = position;
    }
    
    public Vector3 GetBasePosition()
    {
        return _basePosition;
    }
    
    public void DeliveredResource()
    {
        OnResourceDelivered?.Invoke(_targetResource);
        _targetResource = null; // важный шаг! (по крайней мере так нейронка сказала)
    }
}