using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeCollectible : Item
{
    float duration = 0;
    // start is set when picking up the item (Inventory-collision)
    float startingTime = 0;

    public void Start()
    {
        duration = Random.Range(15, 31);
    }
    public override Attributes Apply(Attributes other)
    {
        if(Time.time < startingTime + duration)
        {
            other.MovementSpeed *= 1.6f;
            other.ProjectileSpeed *= 1.4f;
            other.AttackDelay *= 0.6f;
        }

        return other;
    }

    public float StartingTime { get => startingTime; set => startingTime = value; }
}
