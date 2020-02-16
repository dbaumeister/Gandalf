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
    GameObject[] definiteLootTable;
    [SerializeField]
    int aboutNumHealingItems;
    [SerializeField]
    GameObject[] possibleHealingItems;
    [SerializeField]
    int aboutNumBoostItems;
    [SerializeField]
    GameObject[] possibleBoostItems;

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
        switch (currentState)
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

    protected virtual void EnterFight()
    {
        if (enemyWaves[currentWave].IsCompleted())
        {
            currentWave++;
            CurrentState = RoomStates.SpawnEnemies;
        }
    }

    void OnEnemyKill()
    {
        if (CurrentState == RoomStates.FightEnemies)
        {
            enemyWaves[currentWave].RemainingEnemies--;
            CurrentState = RoomStates.FightEnemies;
        }
    }

    void SpawnEnemies()
    {
        if (currentWave < enemyWaves.Count && SpawnPoints.Count > 0)
        {
            // Spawn Enemies
            EnemyWave enemyWave = enemyWaves[currentWave];
            for (int i = 0; i < enemyWave.RemainingEnemies; ++i)
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
        if (!spawnedLoot)
        {
            //Add additional healing items if the party took to much damage
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
        switch (pos)
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


    protected virtual IList<EnemyWave> CreateWaves()
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

    protected virtual IList<GameObject> CreateLoot()
    {
        IList<GameObject> loot = new List<GameObject>();
        if (definiteLootTable.Length == 0 && aboutNumBoostItems == 0 && aboutNumHealingItems == 0) {
            return loot;
        }

        int numHealingItems = Mathf.Max(0, aboutNumHealingItems - Random.Range(-1, 1));
        int numBoostItems = Mathf.Max(0, aboutNumBoostItems - Random.Range(-1, 1));

        //Add the definitiveLoot
        if (definiteLootTable.Length != 0)
        {
            for (int i = 0; i < definiteLootTable.Length; i++)
            {
                loot.Add(definiteLootTable[i]);
            }
        }

        //Add the healing items
        //Items at the beginning of the list are added with a higher probability
        if(numHealingItems != 0)
        {
            //Build a probability generator (sort of)
            List<int> probabilities = new List<int>();
            for(int i = 0; i < possibleHealingItems.Length; i++)
            {
                for(int j = 0; j <= Mathf.Max(0, 8 - 2*i); j++)
                {
                    probabilities.Add(i);
                }
            } 
            
            //Add healing items by picking one of the items in the generator randomly
            for(int i = 0; i < numHealingItems; i++)
            {
                int index = Random.Range(0, probabilities.Count);
                loot.Add(possibleHealingItems[probabilities[index]]);
            }
            
        }

        //Add the boost items
        //Items at the beginning of the list are added with a higher probability
        if (numBoostItems != 0)
        {
            //Build a probability generator (sort of)
            List<int> probabilities = new List<int>();
            for (int i = 0; i < possibleBoostItems.Length; i++)
            {
                for (int j = 0; j <= Mathf.Max(0, 8 - 2 * i); j++)
                {
                    probabilities.Add(i);
                }
            }

            //Add boost items by picking one of the items in the generator randomly
            for (int i = 0; i < numBoostItems; ++i)
            {
                int index = Random.Range(0, probabilities.Count);
                loot.Add(possibleBoostItems[probabilities[index]]);
            }

        }


        //for (int i = 0; i < count; ++i)
        //{
        //    int index = Random.Range(0, definiteLootTable.Length);
        //    loot.Add(definiteLootTable[index]);
        //}

        return loot;
    }

    protected virtual IList<Transform> GetSpawnPoints()
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
