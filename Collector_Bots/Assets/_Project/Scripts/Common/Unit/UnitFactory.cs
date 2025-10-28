using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;

    public Unit FactoryMethod()
    {
        var unit = GameObject.Instantiate(_unitPrefab);
        unit.Init(new UnitIdleState(unit), new GetResourceState(unit), new ReturnToBaseState(unit));
        return unit;
    }
}
