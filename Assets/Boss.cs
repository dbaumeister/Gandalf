using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    float groundHealth = 100;
    [SerializeField]
    float flightHealth = 100;
    [SerializeField]
    float plumpsHealth = 100;

    public delegate void BossKilled();
    public event BossKilled OnBossKilled;

    bool vulnerable = true;

    public float FlightHealth { get => flightHealth; set => flightHealth = value; }
    public float PlumpsHealth { get => plumpsHealth; set => plumpsHealth = value; }
    public bool Vulnerable { get => vulnerable; set => vulnerable = value; }
    public float GroundHealth { get => groundHealth; set => groundHealth = value; }

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("TODO: Deal Damage to player or take damage from player");
    }

    void GroundTakeDamage(float damage)
    {
        if (!Vulnerable) return;

        GroundHealth -= damage;

        if (GroundHealth <= 0)
        {
            GroundHealth = 0;
            animator.SetBool("Boss_Flight_Enter_Stage", true);
        }

        animator.SetTrigger("Boss_Ground_Take_Damage");
    }

    void FlightTakeDamage(float damage)
    {
        if (!Vulnerable) return;

        FlightHealth -= damage;

        if (FlightHealth <= 0)
        {
            FlightHealth = 0;
            animator.SetBool("Boss_Plumps_Enter_Stage", true);
        }

        animator.SetTrigger("Boss_Flight_Take_Damage");
    }

    void PlumpsTakeDamage(float damage)
    {
        if (!Vulnerable) return;

        PlumpsHealth -= damage;

        if (PlumpsHealth <= 0)
        {
            PlumpsHealth = 0;
            animator.SetBool("Boss_Plumps_Die", true);
        }

        animator.SetTrigger("Boss_Plumps_Take_Damage");
    }

    void DieAnimationEnded()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if(OnBossKilled != null)
        {
            OnBossKilled();
        }
    }
}
