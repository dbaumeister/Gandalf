using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoozeCollectible : Item
{
    float duration = 0;
    float hangover = 0;
    // start is set when picking up the item (Inventory-collision)
    float startingTime = 0;

    public void Start()
    {
        duration = 60;
        hangover = 60;
    }
    public override Attributes Apply(Attributes other)
    {
        if (Time.time < startingTime + duration)
        {
            other.MovementSpeed += 4f;
            other.ProjectileSpeed += 4f;
            other.AttackDelay *= 0.2f;
        }

        else
        {
            if (Time.time < startingTime + duration + hangover)
            {
                other.MovementSpeed *= 0.5f;
                other.ProjectileSpeed *= 0.5f;
                other.AttackDelay += 0.3f;
            }
        }

        return other;
    }

    public float StartingTime { get => startingTime; set => startingTime = value; }
}
