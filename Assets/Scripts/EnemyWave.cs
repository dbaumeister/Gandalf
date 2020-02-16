using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave
{
    int remainingEnemies;

    public int RemainingEnemies { get => remainingEnemies; set => remainingEnemies = value; }

    public virtual bool IsCompleted()
    {
        return RemainingEnemies <= 0;
    }
}
