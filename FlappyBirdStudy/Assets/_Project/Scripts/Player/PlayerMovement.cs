using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _jumpForce;

    private void OnEnable()
    {
        _inputService.Jump += OnJumped;
    }

    private void OnDisable()
    {
        _inputService.Jump -= OnJumped;
    }

    private void OnJumped()
    {
        Debug.Log("Jump");
        // _rb.AddForce(new Vector2(_rb.linearVelocity.x, _jumpForce) * _rb.mass, ForceMode2D.Impulse);
        _rb.linearVelocityY = _jumpForce;
    }
}
