using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballCollectible : Item
{
    public override Attributes Apply(Attributes other)
    {
        other.AttackDelay += 0.5f;
        other.WeaponDamage += 10;
        return other;
    }
}
