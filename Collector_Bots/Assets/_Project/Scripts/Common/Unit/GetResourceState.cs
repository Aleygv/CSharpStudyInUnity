using UnityEngine;

public class GetResourceState : UnitWalkState
{
    // private Unit _unit;

    public GetResourceState(Unit unit) : base(unit)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Unit.MarkAsBusy(true);
        Resource resource = Unit.GetTargetResource();
        if (resource != null)
        {
            Unit.SetTarget(resource.transform.position);
        }
        else
        {
            // Ресурс исчез — возвращаемся в idle
            // __unit.SetState(__unit.IdleState);
            Unit.EnterState<UnitIdleState>();
        }
    }

    protected override void OnReachedTarget()
    {
        Resource resource = Unit.GetTargetResource();
        if (resource != null)
        {
            // __unit.GetResource(resource);
            Unit.CarryResource(resource);
            // __unit.SetState(__unit.ReturnState);
            Unit.EnterState<ReturnToBaseState>();
        }
        else
        {
            // __unit.SetState(__unit.IdleState);
            Unit.EnterState<UnitIdleState>();
        }
    }
}