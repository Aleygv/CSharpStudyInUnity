using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public event Action<Enemy> OnDied; 
    
    [SerializeField] private EnemyShooter _shooter;
    // [SerializeField] private UI_Score _uiScore;

    public void Init(ObjectPool<Bullet> bulletPool)
    {
        _shooter?.Init(bulletPool);
    }
    
    public void TakeDamage()
    {
        OnDied?.Invoke(this);
        // _uiScore.ChangeScore();
    }
}