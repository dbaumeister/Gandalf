using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField]
    string name = "Npc";

    [SerializeField]
    string message = null;

    public string Name { get => name; }
    public string Message { get => message; }
}
