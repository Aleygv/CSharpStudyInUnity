using UnityEngine;

public interface IUnitState
{
    void Enter(Unit unit);
    void Exit(Unit unit);
    void Update(Unit unit);
}
