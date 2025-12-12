using System;
using System.Collections;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    [SerializeField] private Vector3 _mapCenter;
    [SerializeField] private Vector3 _mapSize;

    private Camera _camera;
    private Bounds _mapBounds;
    private Vector3 _initialPosition;
    private Vector3 _mOffset = new Vector3(0, 0, 0.5f);
    private bool _isDragging = false;
    private float _mZCoord;

    public event Action<bool> OnFlagHasNewPosition;

    public void Init(Camera mainCamera)
    {
        _camera = mainCamera;
    }

    private void Awake()
    {
        _mapBounds = new Bounds(_mapCenter, _mapSize);
    }

    private void Start()
    {
        _initialPosition = gameObject.transform.position;
    }

    private void CheckMapBounds(Vector3 point)
    {
        if (_mapBounds.Contains(point))
        {
            Debug.Log("На карте");
            OnFlagHasNewPosition?.Invoke(true);
            _isDragging = false;
        }
        else
        {
            OnFlagHasNewPosition?.Invoke(false);
            transform.position = _initialPosition;
            _isDragging = false;
            Debug.Log("Вне карты");
        }
    }

    public void OnMouseDownOnBase()
    {
        // Если объект был зафиксирован, первый клик начинает перетаскивание
        _isDragging = true;
        _mZCoord = _camera.WorldToScreenPoint(gameObject.transform.position).z;
        Debug.Log("Начато перетаскивание.");
    }

    private void OnMouseDown()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, 100f);
        Vector3 pointOnGround = hit.point;
        if (_isDragging)
        {
            CheckMapBounds(pointOnGround);
        }
    }

    void Update()
    {
        // Перемещаем объект только если флаг isDragging установлен в true
        if (_isDragging)
        {
            transform.position = GetMouseAsWorldPoint() + _mOffset;
        }
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _mZCoord;
        return _camera.ScreenToWorldPoint(mousePoint);
    }
}