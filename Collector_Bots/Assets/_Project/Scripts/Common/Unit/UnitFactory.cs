using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;

    private Dictionary<Type, IUnitState> _states;

    private Dictionary<Type, IUnitState> CreateStates()
    {
        return new Dictionary<Type, IUnitState>()
            {
                {typeof(UnitIdleState), new UnitIdleState()},
                {typeof(GetResourceState), new GetResourceState()},
                {typeof(ReturnToBaseState), new ReturnToBaseState()}
            };
    }
    
    public Unit FactoryMethod()
    {
        var unit = GameObject.Instantiate(_unitPrefab);
        unit.Init(CreateStates());
        return unit;
    }
}
