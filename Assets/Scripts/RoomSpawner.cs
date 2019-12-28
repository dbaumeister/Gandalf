using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField]
    RoomLayout[] bossRoomLayouts;

    [SerializeField]
    RoomLayout[] fightRoomLayouts;

    [SerializeField]
    RoomLayout[] shopRoomLayouts;

    [SerializeField]
    RoomLayout[] specialRoomLayouts;

    [SerializeField]
    RoomLayout[] startRoomLayouts;

    RoomLayout layout = null;

    public Door NorthDoor()
    {
        return layout.NorthDoor;
    }

    public Door SouthDoor()
    {
        return layout.SouthDoor;
    }

    public Door EastDoor()
    {
        return layout.EastDoor;
    }

    public Door WestDoor()
    {
        return layout.WestDoor;
    }

    void InitializeLayout(RoomLayout[] objs)
    {
        int i = Random.Range(0, objs.Length);
        if (i < objs.Length)
        {
            layout = Instantiate(objs[i], transform);
        }
    }

    public void Initialize(RoomType type)
    {
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

    public IList<Transform> GetSpawnPoints()
    {
        IList<Transform> positions = new List<Transform>();
        if(!layout)
        {
            return positions;
        }

        GameObject[] objs = layout.SpawnPoints;
        for(int i = 0; i < objs.Length; ++i)
        {
            positions.Add(objs[i].transform);
        }
        return positions;
    }
}
