using System;
using UnityEngine;

public class PainterTask2 : MonoBehaviour
{
    private PlayerTask2 _player;
    
    private void DrawPlayer(PlayerTask2 player)
    {
        Debug.Log($"Draw x cord - {player.X}");
        Debug.Log($"Draw y cord - {player.Y}");
    }

    //Проверка
    private void Start()
    {
        _player = GetComponent<PlayerTask2>();
        DrawPlayer(_player);
    }
}
