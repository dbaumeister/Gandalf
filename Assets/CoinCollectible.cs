using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectible : Item
{
    public override Attributes Apply(Attributes other)
    {
        other.ProjectileSize += 1;
        return other;
    }
}
