using UnityEngine;

public class BaseBuildingState : IBaseState
{
    private readonly Base _base;
    
    public BaseBuildingState(Base new_base)
    {
        _base = new_base;
    }
    
    public void Enter()
    {
        _base.ResourcesForNewBase = 0;
    }

    public void Exit()
    {
        // _base.EnterState<BaseIdleState>();
    }

    public void Update()
    {
        if (_base.ResourcesForNewBase < 5)
        {
            _base.OrganizeAppearance();
        }
        else
        {
            _base.SendUnitToFlag();
        }
    }
}
