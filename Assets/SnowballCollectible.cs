using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballCollectible : Item
{
    public override Attributes Apply(Attributes other)
    {
        other.ProjectileSize += 1f;
        other.AttackDelay *= 1.5f;
        other.WeaponDamage += 10;
        return other;
    }
}
