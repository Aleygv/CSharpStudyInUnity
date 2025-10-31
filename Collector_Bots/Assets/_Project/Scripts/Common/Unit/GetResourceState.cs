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
        _unit.MarkAsBusy(true);
        Resource resource = _unit.GetTargetResource();
        if (resource != null)
        {
            _unit.SetTarget(resource.transform.position);
        }
        else
        {
            // Ресурс исчез — возвращаемся в idle
            // __unit.SetState(__unit.IdleState);
            _unit.EnterState<UnitIdleState>();
        }
    }

    protected override void OnReachedTarget()
    {
        Resource resource = _unit.GetTargetResource();
        if (resource != null)
        {
            // __unit.GetResource(resource);
            _unit.CarryResource(resource);
            // __unit.SetState(__unit.ReturnState);
            _unit.EnterState<ReturnToBaseState>();
        }
        else
        {
            // __unit.SetState(__unit.IdleState);
            _unit.EnterState<UnitIdleState>();
        }
    }
}