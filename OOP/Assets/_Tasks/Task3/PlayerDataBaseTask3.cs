using System.Collections.Generic;
using UnityEngine;

public class PlayerDataBaseTask3
{
    private List<PlayerTask3> _players;

    private void AddPlayer(PlayerTask3 player)
    {
        _players.Add(player);
    }

    private void BanPlayer(int id)
    {
        FindPlayer(id).IsBanned = true;
    }

    private void UnbanPlayer(int id)
    {
        FindPlayer(id).IsBanned = false;
    }

    private void RemovePlayer(PlayerTask3 player)
    {
        _players.Remove(player);
    }

    private PlayerTask3 FindPlayer(int id)
    {
        return _players.Find(p => p.Id == id);
    }
}
