using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AlmanachCollectible : Item
{
    int choice;

    //float newIntervalStart = 0;
    //float newIntervalEnd = 0;


    public void Start()
    {
        choice = Random.Range(0, 2);
    }

    // Every few seconds changes the direction of Movement or Projectiles for a few seconds
    public override Attributes Apply(Attributes other)
    {

        switch (choice)
        {
            case 0:
                //orientation of the projectile
                other.ProjectileSpeed *= -1f;
                break;
            case 1:
                //movement-direction
                other.MovementSpeed *= -1f;
                break;
        }

        // add some sugar to sweeten the deal
        other.AttackDelay *= 0.8f;
        return other;

    //{
    //    if(Time.time > newIntervalStart)
    //    {
    //        // Roll a new choice and a new interval duration
    //        newIntervalEnd = Time.time + Random.Range(1, 3);
    //        newIntervalStart = newIntervalEnd + Random.Range(5, 10);
    //        choice = Random.Range(0, 3);
    //    }

    //    if(Time.time < newIntervalEnd)
    //    {
    //        // Apply choice
    //        switch (choice)
    //        {
    //            case 0:
    //                other.MovementSpeed *= -0.1f;
    //                break;
    //            case 1:
    //                other.ProjectileSpeed *= -1;
    //                break;
    //        }

    //        // Apply some perks to compensate
    //        other.MovementSpeed *= 1.1f;
    //        other.AttackDelay *= 0.9f;
    //    }
    //    return other;
    }
}
