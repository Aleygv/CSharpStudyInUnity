using System;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private ScoreView _scoreView;

    private int _score;

    public event Action<int> OnResourceDelivered;
    
    private void Awake()
    {
        // Инициализируем отображение при старте
        if (_scoreView != null)
            _scoreView.SetScore(_score);
    }

    public void AddPoints(int points)
    {
        _score += points;
        _scoreView?.SetScore(_score);
        OnResourceDelivered?.Invoke(_score);
    }

    public void SubtractPoint(int points)
    {
        _score -= points;
        _scoreView?.SetScore(_score);
    }

    public int GetScore() => _score;
}
