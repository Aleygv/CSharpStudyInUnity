using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Base : MonoBehaviour
{
    private const float TIME_PER_ASSIGN = 2.5f;
    private const int UNIT_PRICE = 3;

    [SerializeField] private int _quantityOfUnits;
    [SerializeField] private GameObject _flagPrefab;

    private BaseFactory _baseFactory;
    private List<Unit> _units;
    private UnitFactory _factory;
    private ResourceScanner _scanner;
    private BaseView _baseView;
    private GameObject _flagInstance;
    private Dictionary<Type, IBaseState> _states;
    private IBaseState _currentState;
    private ScoreController _scoreController;
    private ResourceDeliveredHandler _deliveredHandler;

    private float _currentTime;
    private bool _isCreateOver = true;
    private bool _isBuildingNewBase = false;

    public int ResourcesForNewBase { get; set; }

    public event Action<Resource> OnResourceDelivered;
    public event Action<bool> OnCanBuildUnit;
    public event Action<float> OnBuildUnit;

    public void Init(UnitFactory factory, ResourceScanner scanner, ScoreController scoreController,
        BaseFactory baseFactory, ResourceDeliveredHandler deliveredHandler)
    {
        _factory = factory;
        _scanner = scanner;
        _baseView = GetComponentInChildren<BaseView>();
        _scoreController = scoreController;
        _units = new List<Unit>();
        _baseFactory = baseFactory;
        _deliveredHandler = deliveredHandler;

        _states = new Dictionary<Type, IBaseState>();
        _states.Add(typeof(BaseIdleState), new BaseIdleState(this));
        _states.Add(typeof(BaseBuildingState), new BaseBuildingState(this));
        _currentState = _states[typeof(BaseIdleState)];
    }

    private void Start()
    {
        for (int i = 0; i < _quantityOfUnits; i++)
        {
            if (_factory == null) Debug.LogError("Factory is NULL!2");
            var unit = _factory.FactoryMethod(transform.position);
            _units.Add(unit);
            unit.OnResourceDelivered += HandleResourceDelivered;
        }

        SetPositionForUnits();

        _scoreController.OnResourceDelivered += OnResourcesUpdated;
        OnCanBuildUnit += _baseView.SetButtonActive;
        OnBuildUnit += _baseView.SetTimerCreateUnit;
        OnResourceDelivered += _deliveredHandler.OnResourceDeliveryHandler;

        if (FindObjectsByType<FlagController>(FindObjectsSortMode.None).Length == 0)
        {
            _flagInstance = Instantiate(_flagPrefab);
            _flagInstance.GetComponent<FlagController>().Init(Camera.main);
            _flagInstance.transform.position = gameObject.transform.position;
            _flagInstance.GetComponent<FlagController>().OnFlagHasNewPosition += OnFlagPlaced;
        }
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
            }
        }
        else
        {
            bool canBuild = currentScore >= UNIT_PRICE && _isCreateOver;
            OnCanBuildUnit?.Invoke(canBuild);
        }
    }

    public void SendUnitToFlag()
    {
        foreach (Unit unit in _units)
        {
            if (!unit.IsBusy)
            {
                unit.MarkAsBusy(true);
                unit.EnterBuildState(this, _flagInstance.transform.position);
            }

            break;
        }
    }

    public void OnUnitArrivedToBuildBase(Unit unit)
    {
        Base newBase = _baseFactory.FactoryMethod(_flagInstance.transform.position);

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
        var unit = _factory.FactoryMethod(transform.position);
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

    public void OnFlagPlaced(bool canPlaced)
    {
        if (canPlaced)
        {
            EnterState<BaseBuildingState>();
        }
        else
        {
            EnterState<BaseIdleState>();
        }
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

    private void SetPositionForUnits()
    {
        foreach (Unit unit in _units)
        {
            unit.SetBasePosition(transform.position);
        }
    }

    private void HandleResourceDelivered(Resource resource)
    {
        OnResourceDelivered?.Invoke(resource);
        AssignTask();
    }

    private void EnterState<T>() where T : IBaseState
    {
        _currentState?.Exit();
        _currentState = _states[typeof(T)];
        _currentState?.Enter();
    }

    private void OnMouseDown()
    {
        if (_flagInstance != null)
        {
            _flagInstance.GetComponent<FlagController>().OnMouseDownOnBase();
        }
    }
}