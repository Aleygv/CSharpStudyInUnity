using System;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Camera _camera;
    
    public void SetScore(int score)
    {
        _scoreText.text = $"Score: {score}";
    }
}
