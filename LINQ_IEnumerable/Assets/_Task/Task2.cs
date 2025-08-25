using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Task2 : MonoBehaviour
{
    private int[] numbers = new int[100];
    private void Start()
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = Random.Range(0, 100);
        }

        IEnumerable<int> result1 = from number in numbers
            where number > 10
            select number;

        bool result2 = numbers.Any(n => n % 5 == 0);

        bool result3 = numbers.Any(p => p > 0);

        bool result4 = numbers.All(l => l > 0);

        foreach (int i in result1)
        {
            Debug.Log(i);
        }
    }
}
