using UnityEngine;

public class BaseIdleState : IBaseState
{
    private readonly Base _base;
    private float _timer = 0f;
    private const float TIME_PER_ASSIGN = 2.5f;
    
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
        _timer += Time.deltaTime;
        if (_timer >= TIME_PER_ASSIGN)
        {
            _base.AssignTask();
            _timer = 0f;
        }
    }
}
