using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//Для четвертого задания
public static class UiAnimations
{
    public static event Action<Image> OnFadeOutCompleted;

    public static IEnumerator AnimateFadeOut(
        Image image,
        float duration,
        Action callback)
    {
        float timer = 0f;
        while (timer < duration)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(1, 0, timer / duration));
            timer += Time.deltaTime;
            yield return null;

            if (timer >= duration)
            {
                callback?.Invoke();
                OnFadeOutCompleted?.Invoke(image);
            }
        }
    }
}

//Для первого задания
// public static class UiAnimations
// {
//     public static IEnumerator AnimateFadeOut(
//         Image image,
//         float duration,
//         Action callback)
//     {
//         float timer = 0f;
//         while (timer < duration)
//         {
//             image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(1, 0, timer / duration));
//             timer += Time.deltaTime;
//             yield return null;
//
//             if (timer >= duration)
//             {
//                 callback?.Invoke();
//             }
//         }
//     }
// }