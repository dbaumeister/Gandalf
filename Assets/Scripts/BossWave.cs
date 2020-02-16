using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWave : EnemyWave
{
    bool bossKilled = false;

    public void OnBossKilled()
    {
        bossKilled = true;
    }

    public override bool IsCompleted()
    {
        return bossKilled;
    }
}
