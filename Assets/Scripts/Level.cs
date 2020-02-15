using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    IList<Room> rooms;

    [SerializeField]
    Room[] bossRooms;

    [SerializeField]
    Room[] fightRooms;

    [SerializeField]
    Room[] shopRooms;

    [SerializeField]
    Room[] specialRooms;

    [SerializeField]
    Room[] startRooms;

    [SerializeField]
    int roomCount = 5;

    [SerializeField]
    SplashScreen splashScreen;

    // Start is called before the first frame update
    void Start()
    {
        rooms = new List<Room>();

        GameObject.FindGameObjectWithTag("GroupValues").GetComponent<GroupValues>().OnPlayersDied += OnPlayersDied;

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

    Room InstantiateRoom(Room[] objs)
    {
        int i = Random.Range(0, objs.Length);
       	Room room = Instantiate(objs[i], transform);
		return room;

    }

    int AddRoom(RoomType type)
    {
		//randomly choose a prefab of the selected room type

		Room room = null;
        switch(type)
        {
            case RoomType.Boss:
                room = InstantiateRoom(bossRooms);
                break;
            case RoomType.Fight:
                room = InstantiateRoom(fightRooms);
                break;
            case RoomType.Shop:
                room = InstantiateRoom(shopRooms);
                break;
            case RoomType.Special:
                room = InstantiateRoom(specialRooms);
                break;
            case RoomType.Start:
                room = InstantiateRoom(startRooms);
                break;
        }
		Debug.Assert(room != null);

        room.gameObject.SetActive(false);
        room.CurrentState = RoomStates.Initial;
        room.Type = type;
        room.name = "Room (" + type + ")";
        room.OnRoomFinished += OnRoomFinished;
        rooms.Add(room);
        return rooms.Count - 1;
    }

    void OnRoomFinished()
    {
        bool allFinished = true;
        foreach(Room room in rooms)
        {
            bool hasNecessaryType = room.Type == RoomType.Fight || room.Type == RoomType.Boss;
            if (hasNecessaryType && !room.RoomDone)
            {
                allFinished = false;
                break;
            }
        }

        if(allFinished)
        {
            splashScreen.ShowSuccess();
        }
    }

    void OnPlayersDied()
    {
        splashScreen.ShowFailure();
    }

    public void Transition(Door door)
    {
        door.From.LeaveRoom();
        door.To.EnterRoom(door.ToPosition);
    }
}
