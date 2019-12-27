using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    IList<Door> doors;

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
    public IList<Door> Doors { get => doors; set => doors = value; }

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

    public void EnterRoom(DoorPosition playerEnter)
    {
        // TODO set up room assets.
        gameObject.SetActive(true);

        CurrentState = RoomStates.SpawnEnemies;
    }

    public void LeaveRoom()
    {
        // TODO delete all required assets.
        gameObject.SetActive(false);
    }
}
