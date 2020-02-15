using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoomLayout))]
public class Room : MonoBehaviour
{
    [SerializeField]
    RoomLayout layout;

    [SerializeField]
    RoomType type;

    [SerializeField]
    GameObject[] lootTable;

    [SerializeField]
    int[] enemiesPerWave;

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

    bool spawnedLoot = false;
    bool roomDone = false;

    public delegate void RoomFinished();
    public event RoomFinished OnRoomFinished;

    public RoomType Type { get => type; set => type = value; }
    public IList<EnemyWave> EnemyWaves { get => enemyWaves; set => enemyWaves = value; }
    public IList<GameObject> Loot { get => loot; set => loot = value; }
    public RoomStates CurrentState { get => currentState; set => SetState(value); }
    public RoomLayout Layout { get => layout; set => layout = value; }
    public IList<Transform> SpawnPoints { get => spawnPoints; set => spawnPoints = value; }
    public bool RoomDone { get => roomDone; set => roomDone = value; }

	public void Awake() {
		//layout
		layout = GetComponent<RoomLayout>();

        spawnPoints = GetSpawnPoints();

		//loot
        loot = CreateLoot();
		//determine waves
        enemyWaves = CreateWaves();
		
	}

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
                Debug.Log("spwaning enemies");
                SpawnEnemies();
                break;
            case RoomStates.SpawnLoot:
                SpawnLoot();
                break;
        }
    }

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

    void FinishedRoom()
    {
        NorthDoor().Open();
        SouthDoor().Open();
        EastDoor().Open();
        WestDoor().Open();

        RoomDone = true;
        if (OnRoomFinished != null)
        {
            OnRoomFinished();
        }
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
        if(CurrentState == RoomStates.FightEnemies)
        {
            enemyWaves[currentWave].RemainingEnemies--;
            CurrentState = RoomStates.FightEnemies;
        }
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


    IList<EnemyWave> CreateWaves()
    {
        IList<EnemyWave> enemyWaves = new List<EnemyWave>();
        int numWaves = enemiesPerWave.Length;
        for (int waveCounter = 0; waveCounter < numWaves; ++waveCounter)
        {
            EnemyWave wave = new EnemyWave();
            wave.RemainingEnemies = enemiesPerWave[waveCounter];
            enemyWaves.Add(wave);
        }

        return enemyWaves;
    }

    IList<GameObject> CreateLoot()
    {
        IList<GameObject> loot = new List<GameObject>();
        if(lootTable.Length == 0) {
            return loot;
        }


        int count = Random.Range(5, 7);
        for(int i = 0; i < count; ++i)
        {
            int index = Random.Range(0, lootTable.Length);
            loot.Add(lootTable[index]);
        }
        return loot;
    }

    private IList<Transform> GetSpawnPoints()
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
