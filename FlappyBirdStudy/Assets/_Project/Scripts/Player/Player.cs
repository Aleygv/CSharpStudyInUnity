using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public event Action OnPlayerDied; 
    
    public void TakeDamage()
    {
        OnPlayerDied?.Invoke();
        gameObject.SetActive(false);
    }
}
