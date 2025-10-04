using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 2;
    [SerializeField] private CharacterTags _tag;
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private float _lifeTimer;
    private float _speed;
    private Vector2 _direction;
    public Action<Bullet> OnHit;

    public void Init(float speed, Vector2 direction)
    {
        _speed = speed;
        _direction = direction;
        _lifeTimer = 0f;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        _lifeTimer += Time.deltaTime;
        
        if (_lifeTimer >= _lifeTime)
        {
            ReturnToPool();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _direction * _speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage();
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        OnHit?.Invoke(this);
    }
}

public enum CharacterTags
{
    Player,
    Enemy
}