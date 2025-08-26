using UnityEngine;

public class PlayerTask1
{
    private int _healthPoints;
    private int _coins;

    public PlayerTask1(int healthPoints = 100, int coins = 0)
    {
        _healthPoints = healthPoints;
        _coins = coins;
    }

    private void OutputPlayerStats()
    {
        Debug.Log($"Curren heath is {_healthPoints}");
        Debug.Log($"Curren coins is {_coins}");
    }
}
