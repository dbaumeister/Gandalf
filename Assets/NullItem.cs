using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullItem : Item
{
    // Start is called before the first frame update
    public override Attributes Apply(Attributes other)
    {
        return other;
    }
}
