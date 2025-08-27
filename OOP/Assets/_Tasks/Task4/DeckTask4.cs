using System.Collections.Generic;
using UnityEngine;

public class DeckTask4
{
    private const int DECK_CAPACITY = 36;
    public List<CardTask4> Cards = new List<CardTask4>();
    private List<string> _suits = new List<string> { "spades", "hearts", "diamonds", "clubs" };
    private List<string> _values = new List<string> { "6", "7", "8", "9", "10", "jack", "queen", "king", "ace" };

    public DeckTask4()
    {
        while (Cards.Count < DECK_CAPACITY)
        {
            AddCard(CreateCard());
        }
    }

    private CardTask4 CreateCard()
    {
        CardTask4 card = new CardTask4(_suits[Random.Range(0, _suits.Count - 1)],
            _values[Random.Range(0, _values.Count - 1)]);
        return card;
    }

    private void AddCard(CardTask4 card)
    {
        if (!Cards.Contains(card) && !(Cards.Count <= DECK_CAPACITY))
        {
            Cards.Add(card);
        }
    }

    public void RefreshDeck()
    {
        DeckTask4 newDeck = new DeckTask4();
        Cards = newDeck.Cards;
    }
}