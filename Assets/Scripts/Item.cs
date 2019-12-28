using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Item : MonoBehaviour
{

    [SerializeField]
    AudioClip sound;

    public AudioClip Sound { get => sound; set => sound = value; }

    abstract public Attributes Apply(Attributes other);
}
