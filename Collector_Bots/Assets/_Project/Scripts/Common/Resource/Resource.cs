using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsReserved { get; set; }

    public bool TryReserve()
    {
        if (IsReserved) return false;
        IsReserved = true;
        return true;
    }
}
