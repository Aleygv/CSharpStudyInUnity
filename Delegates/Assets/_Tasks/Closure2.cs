using System;
using UnityEngine;

//Для задания 5: В
public class Closure2 : MonoBehaviour
{
    public static event Action OnGameStarted;

    private void Start()
    {
        OnGameStarted += () => Debug.Log("Sub");
        //Я считаю, что объект никогда не будет уничтожен сборщиком мусора, так как
        //тут нет отписки от события при уничтожении (и вообще).
        //Я бы вместо лямбды использовал подписку на метод и в методе "OnDestroy"
        //отписывался бы.
        
        OnGameStarted += Task;
        //Как лучше подписаться

        Action handler = () => Debug.Log("Handler");
        OnGameStarted += handler;
        OnGameStarted -= handler;
    }

    private void OnDestroy()
    {
        OnGameStarted -= (() => Debug.Log("Sub"));
        //Но так работать не будет

        OnGameStarted -= Task;
        //Вот так должно работать
    }

    private void Task()
    {
        Debug.Log("Sup");
    }
}
