using UnityEngine;

public class ReturnToBaseState : UnitWalkState
{
    // private Unit _unit; Нельзя в наследнике это дублировать
    public ReturnToBaseState(Unit unit) : base(unit)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Unit.SetTarget(Unit.GetBasePosition());
    }

    public override void Update()
    {
        base.Update();
        if (Unit.GetTargetResource() != null)
        {
            Unit.CarryResource(Unit.GetTargetResource());
        }
    }

    protected override void OnReachedTarget()
    {
        Unit.DeliveredResource();
        // _unit.SetState(_unit.IdleState);
        Unit.EnterState<UnitIdleState>();
    }
}