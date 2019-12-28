using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    RoomSpawner layout;

    [SerializeField]
    RoomType type;

    [SerializeField]
    IList<EnemyWave> enemyWaves;
    int currentWave = 0;

    [SerializeField]
    IList<GameObject> loot;

    [SerializeField]
    RoomStates currentState = RoomStates.Initial;

    [SerializeField]
    IList<Transform> spawnPoints;

    [SerializeField]
    Enemy_SearchTarget[] enemyPrefabs;

    public RoomType Type { get => type; set => type = value; }
    public IList<EnemyWave> EnemyWaves { get => enemyWaves; set => enemyWaves = value; }
    public IList<GameObject> Loot { get => loot; set => loot = value; }
    public RoomStates CurrentState { get => currentState; set => SetState(value); }
    public RoomSpawner Layout { get => layout; set => layout = value; }
    public IList<Transform> SpawnPoints { get => spawnPoints; set => spawnPoints = value; }

    bool spawnedLoot = false;

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

    public Door NorthDoor()
    {
        return layout.NorthDoor();
    }

    public Door SouthDoor()
    {
        return layout.SouthDoor();
    }

    public Door EastDoor()
    {
        return layout.EastDoor();
    }

    public Door WestDoor()
    {
        return layout.WestDoor();
    }

    void FinishedRoom()
    {
        Debug.Log("TODO: Finished Room");
        NorthDoor().Open();
        SouthDoor().Open();
        EastDoor().Open();
        WestDoor().Open();
    }

    void EnterFight()
    {
        if(enemyWaves[currentWave].RemainingEnemies <= 0)
        {
            currentWave++;
            CurrentState = RoomStates.SpawnEnemies;
        }
    }

    void OnEnemyKill()
    {
        enemyWaves[currentWave].RemainingEnemies--;
        CurrentState = RoomStates.FightEnemies;
    }

    void SpawnEnemies()
    {
        if(currentWave < enemyWaves.Count && SpawnPoints.Count > 0)
        {
            // Spawn Enemies
            EnemyWave enemyWave = enemyWaves[currentWave];
            for(int i = 0; i < enemyWave.RemainingEnemies; ++i)
            {
                int spawnPoint = Random.Range(0, SpawnPoints.Count);
                int enemy = Random.Range(0, enemyPrefabs.Length);
                Enemy_SearchTarget target = Instantiate(enemyPrefabs[enemy], spawnPoints[spawnPoint].position, Quaternion.identity, transform);
                target.DieCallback += OnEnemyKill;
            }

            CurrentState = RoomStates.FightEnemies;
        }
        else
        {
            CurrentState = RoomStates.SpawnLoot;
        }
    }

    void SpawnLoot()
    {
        if(!spawnedLoot)
        {
            foreach (GameObject obj in Loot)
            {
                int spawnPoint = Random.Range(0, SpawnPoints.Count);
                Instantiate(obj, spawnPoints[spawnPoint].position, Quaternion.identity, transform);
            }
            spawnedLoot = true;
        }
        CurrentState = RoomStates.Done;
    }

    Door GetDoor(DoorPosition pos)
    {
        switch(pos)
        {
            case DoorPosition.East:
                return EastDoor();
            case DoorPosition.West:
                return WestDoor();
            case DoorPosition.North:
                return NorthDoor();
            case DoorPosition.South:
                return SouthDoor();
        }
        return null;
    }

    public void EnterRoom(DoorPosition playerEnter)
    {
        gameObject.SetActive(true);

        // Get all players and move them to the spawn points.
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        Door door = GetDoor(playerEnter);
        int maxCount = door.transform.childCount;
        for (int i = 0; i < players.Length; ++i)
        {
            Transform child = door.transform.GetChild(i % maxCount);
            players[i].transform.position = child.position;
        }

        foreach(EnemyWave wave in enemyWaves)
        {
            wave.RemainingEnemies = Random.Range(2, 5);
        }

        NorthDoor().Close();
        SouthDoor().Close();
        EastDoor().Close();
        WestDoor().Close();

        CurrentState = RoomStates.SpawnEnemies;
    }

    public void LeaveRoom()
    {
        gameObject.SetActive(false);
    }
}
