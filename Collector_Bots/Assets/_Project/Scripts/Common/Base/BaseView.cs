using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _createTimer;
    [SerializeField] private TextMeshProUGUI _resourceForNewBaseCounter;
    
    public void Init()
    {
        _button.gameObject.SetActive(false);
        _createTimer.gameObject.SetActive(false);
        _resourceForNewBaseCounter.gameObject.SetActive(false);
    }

    public void SetButtonActive(bool canActive)
    {
        _button.gameObject.SetActive(canActive);
        
    }

    public void SetTimerCreateUnit(float time)
    {
        _createTimer.gameObject.SetActive(true);
        StartCoroutine(FillTimer(time));
    }

    public void SetResourceForNewBaseCounter(int count)
    {
        _resourceForNewBaseCounter.gameObject.SetActive(true);
        _resourceForNewBaseCounter.text = $"Ресурсов: {count.ToString()}";
    }

    private IEnumerator FillTimer(float duration)
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _createTimer.fillAmount = elapsedTime / duration;

            float progress = Mathf.Clamp01(elapsedTime / duration);
            _createTimer.color = new Color(Mathf.Lerp(1, 0, progress), Mathf.Lerp(0, 1, progress), 0);
            yield return null; // ждет следующего кадра
        }
        
        _createTimer.gameObject.SetActive(false);
    }
}
