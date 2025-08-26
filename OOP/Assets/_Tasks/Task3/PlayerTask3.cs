using UnityEngine;

public class PlayerTask3
{
    private int _id;
    private string _nickName;
    private int _level;
    private bool _isBanned;

    public int Id => _id;
    public string NickName => _nickName;
    public int Level => _level;

    public bool IsBanned
    {
        get
        {
            return _isBanned;
        }
        set
        {
            _isBanned = value;
        }
    }

    public PlayerTask3(string nickName)
    {
        _id = Random.Range(100000, 999999);
        _nickName = nickName;
        _level = 0;
        _isBanned = false;
    }
}
