using UnityEngine;

public abstract class UnitWalkState : IUnitState
{
    private Unit _unit;
    public Unit Unit => _unit;
    protected bool _hasReachedTarget = false;
    
    public UnitWalkState(Unit unit)
    {
        _unit = unit;
    }

    public virtual void Enter()
    {
        _hasReachedTarget = false;
    }

    public virtual void Exit()
    {
        Unit.SetTarget(Unit.GetBasePosition());
    }

    public virtual void Update()
    {
        if (!_hasReachedTarget && Unit.Navigator.HasReachedTarget())
        {
            _hasReachedTarget = true;
            OnReachedTarget();
        }
    }

    protected abstract void OnReachedTarget();
}