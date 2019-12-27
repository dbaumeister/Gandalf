using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayout : MonoBehaviour
{
    [SerializeField]
    GameObject[] spawnPoints;

    public GameObject[] SpawnPoints { get => spawnPoints; set => spawnPoints = value; }
}
