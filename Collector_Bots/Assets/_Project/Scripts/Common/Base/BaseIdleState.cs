using UnityEngine;

public class BaseIdleState : IBaseState
{
    private readonly Base _base;
    
    public BaseIdleState(Base new_base)
    {
        _base = new_base;
    }
    
    public void Enter()
    {
        
    }

    public void Exit()
    {
    }

    public void Update()
    {
        _base.OrganizeAppearance();
    }
}
