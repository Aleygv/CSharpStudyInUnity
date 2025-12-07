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
        _base.AssignTask();
    }

    public void Exit()
    {
        _base.EnterState<BaseIdleState>();
    }

    public void Update()
    {
    }
}
