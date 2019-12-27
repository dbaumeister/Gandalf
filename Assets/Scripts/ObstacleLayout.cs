using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLayout : MonoBehaviour
{
    [SerializeField]
    GameObject[] bossRoomLayouts;

    [SerializeField]
    GameObject[] fightRoomLayouts;

    [SerializeField]
    GameObject[] shopRoomLayouts;

    [SerializeField]
    GameObject[] specialRoomLayouts;

    [SerializeField]
    GameObject[] startRoomLayouts;

    void InitializeLayout(GameObject[] objs)
    {
        int i = Random.Range(0, objs.Length);
        if (i < objs.Length)
            Instantiate(objs[i], transform);
    }

    public void Initialize(RoomType type)
    {
        int i = 0;
        switch(type)
        {
            case RoomType.Boss:
                InitializeLayout(bossRoomLayouts);
                break;
            case RoomType.Fight:
                InitializeLayout(fightRoomLayouts);
                break;
            case RoomType.Shop:
                InitializeLayout(shopRoomLayouts);
                break;
            case RoomType.Special:
                InitializeLayout(specialRoomLayouts);
                break;
            case RoomType.Start:
                InitializeLayout(startRoomLayouts);
                break;
        }
    }
}
