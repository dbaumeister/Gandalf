using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    enum RoomType
    {
        Fight,
        Shop,
        Special,
        Boss
    }

    enum RoomStates
    {
        SpawnEnemies,
        SpawnLoot,
        FightEnemies,
        Idle,
        PlayerAbsent
    }

    [SerializeField]
    int index; // In room list of level

    [SerializeField]
    RoomType type;

    [SerializeField]
    IList<EnemyWave> enemyWaves;

    [SerializeField]
    IList<GameObject> loot;

    [SerializeField]
    RoomStates currentState;

}
