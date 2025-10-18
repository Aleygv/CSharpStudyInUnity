using UnityEngine;

public abstract class UnitWalkState : IUnitState
{
    protected readonly Unit _unit;
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
        _unit.SetTarget(_unit.GetBasePosition());
    }

    public virtual void Update()
    {
        if (!_hasReachedTarget && _unit._navigator.HasReachedTarget())
        {
            _hasReachedTarget = true;
            OnReachedTarget();
        }
    }

    protected abstract void OnReachedTarget();
}