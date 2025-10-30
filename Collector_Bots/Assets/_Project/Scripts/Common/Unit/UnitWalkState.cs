using UnityEngine;

public abstract class UnitWalkState : IUnitState
{
    protected bool _hasReachedTarget = false;

    public UnitWalkState()
    {
    }

    public virtual void Enter(Unit unit)
    {
        _hasReachedTarget = false;
    }

    public virtual void Exit(Unit unit)
    {
        unit.SetTarget(unit.GetBasePosition());
    }

    public virtual void Update(Unit unit)
    {
        if (!_hasReachedTarget && unit.Navigator.HasReachedTarget())
        {
            _hasReachedTarget = true;
            OnReachedTarget(unit);
        }
    }

    protected abstract void OnReachedTarget(Unit unit);
}