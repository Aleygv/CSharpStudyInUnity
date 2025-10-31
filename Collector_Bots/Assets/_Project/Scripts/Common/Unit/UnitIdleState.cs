using UnityEngine;

public class UnitIdleState : IUnitState
{
    private readonly Unit _unit;

    public UnitIdleState(Unit unit)
    {
        _unit = unit;
    }
    public void Enter()
    {
        _unit.MarkAsBusy(false);
        _unit.SetTarget(_unit.GetBasePosition());
    }

    public void Exit()
    {
    }

    public void Update()
    {
        
    }
}
