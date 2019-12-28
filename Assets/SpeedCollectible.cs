﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCollectible : Item
{
    public override Attributes Apply(Attributes other)
    {
        other.MovementSpeed += 1;
        return other;
    }
}
