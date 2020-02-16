using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public delegate void BossKilled();
    public event BossKilled OnBossKilled;

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject);
    }

    void OnDestroy()
    {
        if(OnBossKilled != null)
        {
            OnBossKilled();
        }
    }
}
