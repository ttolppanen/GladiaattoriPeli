using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject
{
    public GameObject enemy;
    public int amount;

    public SpawnableObject(GameObject enemy, int amount)
    {
        this.enemy = enemy;
        this.amount = amount;
    }
}
