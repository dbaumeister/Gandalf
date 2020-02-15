using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoozeCollectible : Item
{
    float duration = 0;
    // start is set when picking up the item (Inventory-collision)
    float startingTime = 0;

    public void Start()
    {
        duration = 45;
    }
    public override Attributes Apply(Attributes other)
    {
        if (Time.time < startingTime + duration)
        {
            other.MovementSpeed *= 1.6f;
            other.ProjectileSpeed *= 1.6f;
            other.AttackDelay *= 0.5f;
        }

        if (Time.time > startingTime + duration)
        {
            other.MovementSpeed *= 0.6f;
            other.ProjectileSpeed *= 0.6f;
            other.AttackDelay *= 1.5f;
        }

        return other;
    }

    public float StartingTime { get => startingTime; set => startingTime = value; }
}
