using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    IList<Room> rooms;

    [SerializeField]
    int roomCount = 5;

    [SerializeField]
    Room roomPrefab;

    [SerializeField]
    GameObject[] lootTable;

    // Start is called before the first frame update
    void Start()
    {
        rooms = new List<Room>();

        // Create all rooms
        int start = AddRoom(RoomType.Start);
        for(int i = 0; i < roomCount; ++i)
        {
            AddRoom(RoomType.Fight);
        }
        AddRoom(RoomType.Boss);
        int shop = AddRoom(RoomType.Shop);
        int special = AddRoom(RoomType.Special);

        // Wire them up
        // The first roomCount + 2 rooms are connected horizontally
        for(int i = 0; i < roomCount + 1; ++i)
        {
            AddHorizontalConnection(rooms[i], rooms[i + 1]);
        }

        // The last two rooms are connected to one of the normal rooms
        int conn = 1 + Random.Range(0, roomCount);
        AddVerticalConnection(rooms[shop], rooms[conn]);

        conn = 1 + Random.Range(0, roomCount);
        AddVerticalConnection(rooms[conn], rooms[special]);

        rooms[start].EnterRoom(DoorPosition.West);
    }

    void AddHorizontalConnection(Room leftRoom, Room rightRoom)
    {
        Door doorInLeftRoom = leftRoom.EastDoor();
        doorInLeftRoom.From = leftRoom;
        doorInLeftRoom.To = rightRoom;
        doorInLeftRoom.FromPosition = DoorPosition.East;
        doorInLeftRoom.ToPosition = DoorPosition.West;
        leftRoom.EastDoor().Show();

        Door doorInRightRoom = rightRoom.WestDoor();
        doorInRightRoom.From = rightRoom;
        doorInRightRoom.To = leftRoom;
        doorInRightRoom.FromPosition = DoorPosition.West;
        doorInRightRoom.ToPosition = DoorPosition.East;
        rightRoom.WestDoor().Show();
    }

    void AddVerticalConnection(Room upperRoom, Room lowerRoom)
    {
        Door doorInUpperRoom = upperRoom.SouthDoor();
        doorInUpperRoom.From = upperRoom;
        doorInUpperRoom.To = lowerRoom;
        doorInUpperRoom.FromPosition = DoorPosition.South;
        doorInUpperRoom.ToPosition = DoorPosition.North;
        upperRoom.SouthDoor().Show();

        Door doorInLowerRoom = lowerRoom.NorthDoor();
        doorInLowerRoom.From = lowerRoom;
        doorInLowerRoom.To = upperRoom;
        doorInLowerRoom.FromPosition = DoorPosition.North;
        doorInLowerRoom.ToPosition = DoorPosition.South;
        lowerRoom.NorthDoor().Show();
    }

    int AddRoom(RoomType type)
    {
        Room room = Instantiate(roomPrefab);;
        room.gameObject.SetActive(false);
        room.CurrentState = RoomStates.Initial;
        room.Type = type;
        room.EnemyWaves = CreateWaves(type);
        room.Loot = CreateLoot(type);
        room.name = "Room (" + type + ")";
        room.Layout.Initialize(type);
        room.SpawnPoints = room.Layout.GetSpawnPoints();
        rooms.Add(room);
        return rooms.Count - 1;
    }

    IList<EnemyWave> CreateWaves(RoomType type)
    {
        IList<EnemyWave> enemyWaves = new List<EnemyWave>();
        if (type == RoomType.Fight)
        {
            int numWaves = Random.Range(1, 3);
            for (int j = 0; j < numWaves; ++j)
            {
                EnemyWave wave = new EnemyWave();
                wave.RemainingEnemies = Random.Range(2, 5);
                enemyWaves.Add(wave);
            }
        }
        else if (type == RoomType.Boss)
        {
            int numWaves = Random.Range(4, 6);
            for (int j = 0; j < numWaves; ++j)
            {
                EnemyWave wave = new EnemyWave();
                wave.RemainingEnemies = Random.Range(4, 7);
                enemyWaves.Add(wave);
            }
        }
        return enemyWaves;
    }

    IList<GameObject> CreateLoot(RoomType type)
    {
        IList<GameObject> loot = new List<GameObject>();
        int count = Random.Range(1, 3);
        for(int i = 0; i < count; ++i)
        {
            int index = Random.Range(0, lootTable.Length);
            loot.Add(lootTable[index]);
        }
        return loot;
    }

    public void Transition(Door door)
    {
        door.From.LeaveRoom();
        door.To.EnterRoom(door.ToPosition);
    }
}
