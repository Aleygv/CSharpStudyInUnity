using UnityEngine;

public class GetResourceState : UnitWalkState
{
    public GetResourceState()
    {
    }

    public override void Enter(Unit unit)
    {
        base.Enter(unit);
        unit.MarkAsBusy(true);
        Resource resource = unit.GetTargetResource();
        if (resource != null)
        {
            unit.SetTarget(resource.transform.position);
        }
        else
        {
            // Ресурс исчез — возвращаемся в idle
            // _unit.SetState(_unit.IdleState);
            unit.EnterState<UnitIdleState>();
        }
    }

    protected override void OnReachedTarget(Unit unit)
    {
        Resource resource = unit.GetTargetResource();
        if (resource != null)
        {
            // _unit.GetResource(resource);
            unit.CarryResource(resource);
            // _unit.SetState(_unit.ReturnState);
            unit.EnterState<ReturnToBaseState>();
        }
        else
        {
            // _unit.SetState(_unit.IdleState);
            unit.EnterState<UnitIdleState>();
        }
    }
}
