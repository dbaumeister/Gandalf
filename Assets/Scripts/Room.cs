using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    GameObject northDoor;

    [SerializeField]
    GameObject southDoor;

    [SerializeField]
    GameObject eastDoor;

    [SerializeField]
    GameObject westDoor;

    [SerializeField]
    RoomType type;

    [SerializeField]
    IList<EnemyWave> enemyWaves;
    int currentWave = 0;

    [SerializeField]
    IList<GameObject> loot;

    [SerializeField]
    RoomStates currentState = RoomStates.Initial;

    public RoomType Type { get => type; set => type = value; }
    public IList<EnemyWave> EnemyWaves { get => enemyWaves; set => enemyWaves = value; }
    public IList<GameObject> Loot { get => loot; set => loot = value; }
    public RoomStates CurrentState { get => currentState; set => SetState(value); }
    public GameObject NorthDoor { get => northDoor; set => northDoor = value; }
    public GameObject SouthDoor { get => southDoor; set => southDoor = value; }
    public GameObject EastDoor { get => eastDoor; set => eastDoor = value; }
    public GameObject WestDoor { get => westDoor; set => westDoor = value; }

    void SetState(RoomStates state)
    {
        RoomStates oldState = currentState;
        currentState = state;
        OnStateChange(oldState, currentState);
    }

    void OnStateChange(RoomStates oldState, RoomStates currentState)
    {
        switch(currentState)
        {
            case RoomStates.Done:
                FinishedRoom();
                break;
            case RoomStates.FightEnemies:
                EnterFight();
                break;
            case RoomStates.Initial:
                break;
            case RoomStates.SpawnEnemies:
                SpawnEnemies();
                break;
            case RoomStates.SpawnLoot:
                SpawnLoot();
                break;
        }
    }

    void FinishedRoom()
    {
        Debug.Log("TODO: Finished Room");
    }

    void EnterFight()
    {
        Debug.Log("TODO: Entered Fight");

        while(enemyWaves[currentWave].RemainingEnemies > 0)
        {
            Debug.Log("Killed Enemy");
            enemyWaves[currentWave].RemainingEnemies--;
        }

        Debug.Log("Killed All Enemies");
        currentWave++;
        CurrentState = RoomStates.SpawnEnemies;
    }

    void SpawnEnemies()
    {
        if(currentWave < enemyWaves.Count)
        {
            Debug.Log("TODO: Spawn Enemies");
            CurrentState = RoomStates.FightEnemies;
        }
        else
        {
            CurrentState = RoomStates.SpawnLoot;
        }
    }

    void SpawnLoot()
    {
        Debug.Log("TODO: Spawn Loot");
        CurrentState = RoomStates.Done;
    }

    GameObject GetDoor(DoorPosition pos)
    {
        switch(pos)
        {
            case DoorPosition.East:
                return EastDoor;
            case DoorPosition.West:
                return WestDoor;
            case DoorPosition.North:
                return NorthDoor;
            case DoorPosition.South:
                return SouthDoor;
        }
        return null;
    }

    public void EnterRoom(DoorPosition playerEnter)
    {
        gameObject.SetActive(true);

        // Get all players and move them to the spawn points.
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        GameObject door = GetDoor(playerEnter);
        int maxCount = door.transform.childCount;
        for (int i = 0; i < players.Length; ++i)
        {
            Transform child = door.transform.GetChild(i % maxCount);
            players[i].transform.position = child.position;
        }

        CurrentState = RoomStates.SpawnEnemies;
    }

    public void LeaveRoom()
    {
        gameObject.SetActive(false);
    }
}
