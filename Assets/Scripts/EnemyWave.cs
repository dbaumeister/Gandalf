using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    [SerializeField]
    IList<GameObject> enemyPrefabs;

    int remainingEnemies = 5;

    public int RemainingEnemies { get => remainingEnemies; set => remainingEnemies = value; }

}
