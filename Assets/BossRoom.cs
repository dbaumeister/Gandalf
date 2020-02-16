using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room
{
    public delegate void BossKilled();
    public event BossKilled OnBossKilled;

    bool bossSpawned = false;

    [SerializeField]
    Boss boss;

    protected override IList<EnemyWave> CreateWaves()
    {
        BossWave wave = new BossWave();
        OnBossKilled += wave.OnBossKilled;
        return new List<EnemyWave> { wave };
    }

    void BossWasKilled()
    {
        if(OnBossKilled != null)
        {
            OnBossKilled();
        }

        CurrentState = RoomStates.FightEnemies;
    }

    protected override void EnterFight()
    {
        if(!bossSpawned)
        {
            Boss b = Instantiate(boss, transform);
            b.OnBossKilled += BossWasKilled;
            bossSpawned = true;
        }

        base.EnterFight();
    }

}
