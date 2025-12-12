using UnityEngine;

public class BaseFactory : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;
    
    [SerializeField] private UnitFactory _factory;
    [SerializeField] private ResourceScanner _scanner;
    [SerializeField] private BaseView _baseView;
    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private ResourceDeliveredHandler _deliveredHandler;
    
    public Base FactoryMethod(Vector3 flagPosition)
    {
        var newBase = Instantiate(_basePrefab, flagPosition, Quaternion.identity);
        newBase.Init(_factory, _scanner, _scoreController, this, _deliveredHandler);
        return newBase;
    }
    
    private void OnEnable()
    {
        Debug.Log("BaseFactory enabled. Prefab: " + (_basePrefab != null ? "OK" : "NULL"));
    }
}
