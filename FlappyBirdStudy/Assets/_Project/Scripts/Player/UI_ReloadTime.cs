using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_ReloadTime : MonoBehaviour
{
    [SerializeField] private Image _reloadImage;

    public void ChangeBarValue(float value)
    {
        _reloadImage.fillAmount = value;
        _reloadImage.color = new Color(value, _reloadImage.color.g + value, _reloadImage.color.b - value);
    }
}