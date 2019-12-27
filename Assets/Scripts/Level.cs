using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    IList<Room> rooms;

    [SerializeField]
    int roomCount = 5;

    GameObject empty;

    IDictionary<Room, Door> incomingConnections;
    IDictionary<Room, Door> outgoingConnections;

    // Start is called before the first frame update
    void Start()
    {
        empty = new GameObject();
        rooms = new List<Room>();

        outgoingConnections = new Dictionary<Room, Door>();
        incomingConnections = new Dictionary<Room, Door>();

        // Create all rooms
        AddRoom(RoomType.Start);
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
        AddVerticalConnection(rooms[conn], rooms[shop]);
    }

    void AddHorizontalConnection(Room leftRoom, Room rightRoom)
    {
        AddConnection(leftRoom, rightRoom, DoorPosition.East, DoorPosition.West);
    }

    void AddVerticalConnection(Room upperRoom, Room lowerRoom)
    {
        AddConnection(upperRoom, lowerRoom, DoorPosition.South, DoorPosition.North);
    }

    void AddConnection(Room leftRoom, Room rightRoom, DoorPosition east, DoorPosition west)
    {
        Door doorInLeftRoom = new Door();
        doorInLeftRoom.From = leftRoom;
        doorInLeftRoom.To = rightRoom;
        doorInLeftRoom.DoorPosition = east;

        Door doorInRightRoom = new Door();
        doorInRightRoom.From = rightRoom;
        doorInRightRoom.To = leftRoom;
        doorInRightRoom.DoorPosition = west;

        outgoingConnections[leftRoom] = doorInLeftRoom;
        outgoingConnections[rightRoom] = doorInRightRoom;
        incomingConnections[leftRoom] = doorInRightRoom;
        incomingConnections[rightRoom] = doorInLeftRoom;

        leftRoom.Doors.Add(doorInLeftRoom);
        rightRoom.Doors.Add(doorInRightRoom);
    }

    int AddRoom(RoomType type)
    {
        GameObject obj = Instantiate<GameObject>(empty);
        Room room = obj.AddComponent<Room>();
        room.CurrentState = RoomStates.Initial;
        room.Type = type;
        room.EnemyWaves = CreateWaves(type);
        room.Loot = CreateLoot(type);
        room.name = "Room (" + type + ")";
        room.Doors = new List<Door>();
        rooms.Add(room);
        return rooms.Count - 1;
    }

    IList<EnemyWave> CreateWaves(RoomType type)
    {
        IList<EnemyWave> enemyWaves = new List<EnemyWave>();
        if (type == RoomType.Fight)
        {
            for (int j = 0; j < 3; ++j) enemyWaves.Add(new EnemyWave());
        }
        else if (type == RoomType.Boss)
        {
            enemyWaves.Add(new EnemyWave());
        }
        return enemyWaves;
    }

    IList<GameObject> CreateLoot(RoomType type)
    {
        // TODO gamble loot
        IList<GameObject> loot = new List<GameObject>();
        return loot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
