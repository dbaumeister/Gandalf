using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCollectible : Item
{
    public override Attributes Apply(Attributes other)
    {
        other.ProjectileSpeed *= 1.5f;
        other.AttackDelay *= 0.7f;
        return other;
    }
}
