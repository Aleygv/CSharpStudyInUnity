using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : MonoBehaviour
{
    [SerializeField] private InputActionAsset _actionAsset;

    private InputAction _jumpAction;
    private InputAction _shootAction;

    public event Action Jump;
    public event Action Shoot;
    
    private void OnEnable()
    {
        _actionAsset.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        _actionAsset.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        _jumpAction = _actionAsset.FindAction("Jump");
        _shootAction = _actionAsset.FindAction("Shoot");
    }

    private void Update()
    {
        if (_jumpAction.WasPressedThisFrame())
        {
            Jump?.Invoke();
        }

        if (_shootAction.WasPressedThisFrame())
        {
            Shoot?.Invoke();
        }
    }
}
