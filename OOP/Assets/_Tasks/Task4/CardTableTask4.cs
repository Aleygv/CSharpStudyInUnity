using System;
using UnityEngine;

public class CardTableTask4 : MonoBehaviour
{
    private DeckTask4 _deck;
    private PlayerTask4 _player;

    private void Start()
    {
        _deck = new DeckTask4();
        _player = GetComponent<PlayerTask4>();
        
        //Игрок набирает карты
        _player.GetCard(_deck);
        _player.GetCard(_deck);
        _player.GetCard(_deck);
        _player.GetCard(_deck);
        _player.GetCard(_deck);
        _player.GetCard(_deck);

        if (_player.GetCard(_deck))
        {
            ShowPlayerCards();
        }
    }

    private void ShowPlayerCards()
    {
        foreach (CardTask4 card in _player.PlayerCars)
        {
            Debug.Log($"{card.Value} {card.Suit} \n");
        }
    }
}
