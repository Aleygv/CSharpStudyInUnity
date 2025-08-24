using System;
using UnityEngine;
using UnityEngine.UI;

//Для четвертого задания
public class CallbackTask : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _duration;
    private Action _fadeOut;

    private void Start()
    {
        _fadeOut += () => _image.gameObject.SetActive(false);
        StartCoroutine(UiAnimations.AnimateFadeOut(_image, _duration, _fadeOut));
    }

    private void OnEnable()
    {
        UiAnimations.OnFadeOutCompleted += HandleFadeOut;
        UiAnimations.OnFadeOutCompleted += (image => Debug.Log(image));
    }

    private void OnDisable()
    {
        UiAnimations.OnFadeOutCompleted -= HandleFadeOut;
        UiAnimations.OnFadeOutCompleted -= (image => Debug.Log(image));
    }

    private void HandleFadeOut(Image image)
    {
        image.gameObject.SetActive(true);
        image.color = Color.red;
    }
}

//Для первого задания
// public class CallbackTask : MonoBehaviour
// {
//     [SerializeField] private Image _image;
//     [SerializeField] private float _duration;
//     private Action _fadeOut;
//     
//     private void Start()
//     {
//         
//         _fadeOut += () => _image.gameObject.SetActive(false);
//         StartCoroutine(UiAnimations.AnimateFadeOut(_image, _duration, _fadeOut));
//     }
// }