using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _scoreValue;
    
    public void ChangeScore()
    {
        _scoreText.text = $"Score: {_scoreValue++}";
    }
}
