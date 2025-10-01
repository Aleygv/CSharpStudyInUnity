using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private event Action<Enemy> _onReturnToPool; 
    
    [SerializeField] private EnemyShooter _shooter;
    // [SerializeField] private UI_Score _uiScore;

    public void Init(Action<Enemy> onDie, DelObjectPool<Bullet> bulletPool)
    {
        _shooter?.Init(bulletPool);

        _onReturnToPool = onDie;
    }
    
    public void TakeDamage()
    {
        gameObject.SetActive(false);
        // _uiScore.ChangeScore();
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        _onReturnToPool?.Invoke(this);
    }
}