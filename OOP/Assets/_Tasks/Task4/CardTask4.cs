using System.Collections.Generic;
using UnityEngine;

public class CardTask4
{
    private string _suit;
    private string _value;

    public string Suit
    {
        get { return _suit; }
        set { _suit = value; }
    }
    
    public string Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public CardTask4(string suit, string value)
    {
        _suit = suit;
        _value = value;
    }
}