using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerTask4
{
    private const int MAX_CARD_QUANTITY = 7;
    private bool _isMaxCards;
    
    public List<CardTask4> PlayerCars = new List<CardTask4>();

    public bool GetCard(DeckTask4 deck)
    {
        if (!(deck.Cards.Count < 1) && PlayerCars.Count < MAX_CARD_QUANTITY)
        {
            PlayerCars.Add(deck.Cards[Random.Range(0, deck.Cards.Count - 1)]);
            _isMaxCards = false;
            return _isMaxCards;
        }

        _isMaxCards = true;
        return _isMaxCards;
    }
}
