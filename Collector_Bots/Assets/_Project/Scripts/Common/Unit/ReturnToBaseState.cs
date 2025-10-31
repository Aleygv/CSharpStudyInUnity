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
        _unit.SetTarget(_unit.GetBasePosition());
    }

    public override void Update()
    {
        base.Update();
        if (_unit.GetTargetResource() != null)
        {
            _unit.CarryResource(_unit.GetTargetResource());
        }
    }

    protected override void OnReachedTarget()
    {
        _unit.DeliveredResource();
        // _unit.SetState(_unit.IdleState);
        _unit.EnterState<UnitIdleState>();
    }
}