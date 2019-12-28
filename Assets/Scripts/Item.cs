using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attributes))]
public class Item : MonoBehaviour
{
    [SerializeField]
    Attributes attributes;

    [SerializeField]
    bool permanent = true;

    private void Start()
    {
        attributes = GetComponent<Attributes>();
    }

    public void Apply(Attributes other)
    {
        other.Apply(attributes);
    }
}
