using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    [SerializeField]
    IList<GameObject> enemyPrefabs;

    int remainingEnemies;

    public int RemainingEnemies { get => remainingEnemies; set => remainingEnemies = value; }
}
