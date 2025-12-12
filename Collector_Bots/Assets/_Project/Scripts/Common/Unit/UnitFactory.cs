using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    
    public Unit FactoryMethod(Vector3 position)
    {
        var unit = Instantiate(_unitPrefab, position, Quaternion.Euler(0, 0, 0));
        unit.Init();
        return unit;
    }
}
