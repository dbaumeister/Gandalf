using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCollectible : Item
{
    public override Attributes Apply(Attributes other)
    {
        other.ProjectileSize += 1;
        other.ProjectileSpeed += 1;
        other.AttackDelay -= 0.5f;
        return other;
    }
}
