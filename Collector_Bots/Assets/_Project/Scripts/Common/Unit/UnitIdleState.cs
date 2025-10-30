using UnityEngine;

public class UnitIdleState : IUnitState
{
    private readonly Unit _unit;

    public UnitIdleState()
    {
    }
    public void Enter(Unit unit)
    {
        unit.MarkAsBusy(false);
        unit.SetTarget(unit.GetBasePosition());
    }

    public void Exit(Unit unit)
    {
    }

    public void Update(Unit unit)
    {
        
    }
}
