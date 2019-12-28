using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    [SerializeField]
    IList<GameObject> enemyPrefabs;

    int remainingEnemies = 1;

    public int RemainingEnemies { get => remainingEnemies; set => remainingEnemies = value; }

    private void Awake()
    {
        RemainingEnemies = Random.Range(1, 2);
    }

}
