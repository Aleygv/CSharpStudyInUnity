using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;

    public Unit FactoryMethod()
    {
        var unit = GameObject.Instantiate(_unitPrefab);
        unit.Init();
        return unit;
    }
}
