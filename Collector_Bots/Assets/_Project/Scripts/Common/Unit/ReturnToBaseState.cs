using UnityEngine;

public class ReturnToBaseState : UnitWalkState
{
    public ReturnToBaseState()
    {
    }

    public override void Enter(Unit unit)
    {
        base.Enter(unit);
        unit.SetTarget(unit.GetBasePosition());
    }

    public override void Update(Unit unit)
    {
        base.Update(unit);
        if (unit.GetTargetResource() != null)
        {
            unit.CarryResource(unit.GetTargetResource());
        }
    }

    protected override void OnReachedTarget(Unit unit)
    {
        unit.DeliveredResource();
        // _unit.SetState(_unit.IdleState);
        unit.EnterState<UnitIdleState>();
    }
}