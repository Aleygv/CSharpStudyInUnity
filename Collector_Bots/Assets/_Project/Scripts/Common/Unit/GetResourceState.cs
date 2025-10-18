using UnityEngine;

public class GetResourceState : UnitWalkState
{
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
            _unit.SetState(new UnitIdleState(_unit));
        }
    }

    protected override void OnReachedTarget()
    {
        Resource resource = _unit.GetTargetResource();
        if (resource != null)
        {
            // _unit.GetResource(resource);
            _unit.CarryResource(resource);
            _unit.SetState(new ReturnToBaseState(_unit));
        }
        else
        {
            _unit.SetState(new UnitIdleState(_unit));
        }
    }
}
