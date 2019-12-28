using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollectible : Item
{
    public override Attributes Apply(Attributes other)
    {
        other.AttackDelay -= 0.02f;
        other.ProjectileSpeed += 2;
        return other;
    }
}
