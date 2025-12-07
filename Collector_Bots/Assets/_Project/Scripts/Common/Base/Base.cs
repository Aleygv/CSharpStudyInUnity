using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Base : MonoBehaviour
{
    private const float TIME_PER_ASSIGN = 2.5f;
    private const int UNIT_PRICE = 3;

    // [SerializeField] private BaseIdleState _idleState;
    // [SerializeField] private BaseScanningState _scanningState;
    [SerializeField] private int _quantityOfUnits;
    [SerializeField] private GameObject _flagPrefab;
    [SerializeField] private GameObject _basePrefab;

    private List<Unit> _units;
    private UnitFactory _factory;
    private ResourceScanner _scanner;
    private BaseView _baseView;
    private GameObject _flagInstance;
    private Dictionary<Type, IBaseState> _states;
    private IBaseState _currentState;
    private ScoreController _scoreController;
    
    private float _currentTime;
    private bool _isCreateOver = true;
    private bool _isBuildingNewBase = false;
    
    public int ResourcesForNewBase { get; set; }
    
    public event Action<Resource> OnResourceDelivered;
    public event Action<bool> OnCanBuildUnit;
    public event Action<float> OnBuildUnit;

    public void Init(UnitFactory factory, ResourceScanner scanner, BaseView baseView, ScoreController scoreController)
    {
        _factory = factory;
        _scanner = scanner;
        _baseView = baseView;
        _scoreController = scoreController;
        _units = new List<Unit>();
        
        _states = new Dictionary<Type, IBaseState>();
        _states.Add(typeof(BaseIdleState), new BaseIdleState(this));
        _states.Add(typeof(BaseBuildingState), new BaseBuildingState(this));
        _currentState = _states[typeof(BaseIdleState)];
    }

    private void Start()
    {
        for (int i = 0; i < _quantityOfUnits; i++)
        {
            var unit = _factory.FactoryMethod();
            _units.Add(unit);
            unit.OnResourceDelivered += HandleResourceDelivered;
        }

        _scoreController.OnResourceDelivered += OnResourcesUpdated;
        OnCanBuildUnit += _baseView.SetButtonActive;
        OnBuildUnit += _baseView.SetTimerCreateUnit;
        
        _flagInstance = Instantiate(_flagPrefab);
        _flagInstance.GetComponent<FlagController>().Init(Camera.main);
        _flagInstance.transform.position = gameObject.transform.position;
        _flagInstance.GetComponent<FlagController>().OnFlagHasNewPosition += OnFlagPlaced;
    }

    private void Update()
    {
        _currentState?.Update();
    }

    private void OnResourcesUpdated(int currentScore)
    {
        if (_currentState is BaseBuildingState && !_isBuildingNewBase)
        {
            ResourcesForNewBase++;
            _baseView.SetResourceForNewBaseCounter(ResourcesForNewBase);
            if (ResourcesForNewBase >= 5)
            {
                _isBuildingNewBase = true;
                SendUnitToFlag();
            }
        }
        else
        {
            bool canBuild = currentScore >= UNIT_PRICE && _isCreateOver;
            OnCanBuildUnit?.Invoke(canBuild);
        }
    }

    private void SendUnitToFlag()
    {
        foreach (Unit unit in _units)
        {
            if (!unit.IsBusy)
            {
                unit.MarkAsBusy(true);
                unit.EnterBuildState(this, _flagInstance.transform.position);
                return;
            }
        }
    }

    public void OnUnitArrivedToBuildBase(Unit unit, Vector3 position)
    {
        GameObject newBaseObj = Instantiate(_basePrefab, position, Quaternion.identity);
        Base newBase = newBaseObj.GetComponent<Base>();
        
        newBase.Init(_factory, _scanner, _baseView, _scoreController);
        newBase.AddUnit(unit);
        _units.Remove(unit);
        Destroy(_flagInstance);
        ResourcesForNewBase = 0;
        _isBuildingNewBase = false;
        EnterState<BaseIdleState>();
    }
    
    public void AddUnit(Unit unit)
    {
        _units.Add(unit);
    }

    public void StartUnitCreation(float time)
    {
        _isCreateOver = false;
        _scoreController.SubtractPoint(UNIT_PRICE);
        StartCoroutine(CreateUnitAfterDelay());
        OnBuildUnit?.Invoke(time);
    }

    private IEnumerator CreateUnitAfterDelay()
    {
        yield return new WaitForSeconds(10);
        var unit = _factory.FactoryMethod();
        _units.Add(unit);
        unit.OnResourceDelivered += HandleResourceDelivered;
        _isCreateOver = true;
    }

    public void OrganizeAppearance()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= TIME_PER_ASSIGN)
        {
            AssignTask();
            _currentTime = 0f;
        }
    }

    public void OnFlagPlaced()
    {
        EnterState<BaseBuildingState>();
    }

    public void AssignTask()
    {
        foreach (Unit unit in _units)
        {
            if (!unit.IsBusy)
            {
                Resource nearest = _scanner.FindNearest(unit.transform.position);
                if (nearest != null && !unit.IsBusy)
                {
                    nearest.IsReserved = true; // ← бронируем
                    unit.GetResource(nearest);
                }
            }
        }
    }

    private void HandleResourceDelivered(Resource resource)
    {
        OnResourceDelivered?.Invoke(resource);
        AssignTask();
    }

    public void EnterState<T>() where T : IBaseState
    {
        _currentState?.Exit();
        _currentState = _states[typeof(T)];
        _currentState?.Enter();
    }
    
    private void OnMouseDown()
    {
        _flagInstance.GetComponent<FlagController>().OnMouseDownOnBase();
    }
}