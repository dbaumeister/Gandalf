using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayout : MonoBehaviour
{
    [SerializeField]
    GameObject[] spawnPoints;

    [SerializeField]
    Door northDoor;

    [SerializeField]
    Door southDoor;

    [SerializeField]
    Door eastDoor;

    [SerializeField]
    Door westDoor;

    public GameObject[] SpawnPoints { get => spawnPoints; set => spawnPoints = value; }
    public Door NorthDoor { get => northDoor; set => northDoor = value; }
    public Door SouthDoor { get => southDoor; set => southDoor = value; }
    public Door EastDoor { get => eastDoor; set => eastDoor = value; }
    public Door WestDoor { get => westDoor; set => westDoor = value; }
}
