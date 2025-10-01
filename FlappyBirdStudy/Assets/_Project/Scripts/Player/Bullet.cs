using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    private const float LIFE_TIME = 2;

    [SerializeField] private CharacterTags _tag;
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private float _lifeTimer;
    private float _speed;
    private Vector2 _direction;
    private Action<Bullet> _onReturnToPool;

    public void Init(float speed, Vector2 direction, Action<Bullet> onReturnToPool)
    {
        _speed = speed;
        _direction = direction;
        _onReturnToPool = onReturnToPool;
        _lifeTimer = 0f;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        _lifeTimer += Time.deltaTime;

        _rigidbody.linearVelocity = _direction * _speed;
        
        if (_lifeTimer >= LIFE_TIME)
        {
            ReturnToPool();
        }
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
        _onReturnToPool?.Invoke(this);
        gameObject.SetActive(false); //Возможно тут прикол
    }
}

public enum CharacterTags
{
    Player,
    Enemy
}