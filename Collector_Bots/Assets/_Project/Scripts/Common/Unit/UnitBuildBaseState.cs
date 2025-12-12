using System;
using UnityEngine;

public class UnitBuildBaseState : UnitWalkState
{
    private Base _originBase;
    private Vector3 _buildPosition;
    
    public UnitBuildBaseState(Unit unit, Base originBase, Vector3 buildPosition) : base(unit)
    {
        _originBase = originBase;
        _buildPosition = buildPosition;
    }

    public override void Enter()
    {
        base.Enter();
        Unit.Navigator.SetTarget(_buildPosition);
    }

    protected override void OnReachedTarget()
    {
        _originBase.OnUnitArrivedToBuildBase(Unit);
    }
}
