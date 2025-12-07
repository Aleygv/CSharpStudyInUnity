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
                {typeof(UnitIdleState), new UnitIdleState(_unitPrefab)},
                {typeof(GetResourceState), new GetResourceState(_unitPrefab)},
                {typeof(ReturnToBaseState), new ReturnToBaseState(_unitPrefab)}
            };
    }
    
    public Unit FactoryMethod()
    {
        var unit = GameObject.Instantiate(_unitPrefab, _unitPrefab.GetBasePosition(), Quaternion.Euler(0, 0, 0));
        unit.Init();
        return unit;
    }
}
