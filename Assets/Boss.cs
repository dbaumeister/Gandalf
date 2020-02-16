using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum Stage
    {
        Ground,
        Flight,
        Plumps
    }

    [SerializeField]
    float groundHealth = 10;
    [SerializeField]
    float flightHealth = 10;
    [SerializeField]
    float plumpsHealth = 10;

    Stage currentStage;

    public delegate void BossKilled();
    public event BossKilled OnBossKilled;

    bool vulnerable = true;

    public float FlightHealth { get => flightHealth; set => flightHealth = value; }
    public float PlumpsHealth { get => plumpsHealth; set => plumpsHealth = value; }
    public bool Vulnerable { get => vulnerable; set => vulnerable = value; }
    public float GroundHealth { get => groundHealth; set => groundHealth = value; }
    public Stage CurrentStage { get => currentStage; set => currentStage = value; }

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            float damage = GameObject.FindGameObjectWithTag("Player").GetComponent<Attributes>().WeaponDamage;
            TakeDamage(damage);
        }

        if(collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GroupValues").GetComponent<GroupValues>().takeHearts(1);
        }
    }

    void TakeDamage(float damage)
    {
        if (!Vulnerable) return;

        switch(CurrentStage)
        {
            case Stage.Ground:
                GroundTakeDamage(damage);
                break;
            case Stage.Flight:
                FlightTakeDamage(damage);
                break;
            case Stage.Plumps:
                PlumpsTakeDamage(damage);
                break;
        }

        Debug.Log("GroundHealth: " + GroundHealth + ", FlightHealth: " + FlightHealth + ", PlumpsHealth: " + PlumpsHealth);
    }

    void GroundTakeDamage(float damage)
    {
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
        PlumpsHealth -= damage;

        if (PlumpsHealth <= 0)
        {
            PlumpsHealth = 0;
            animator.SetBool("Boss_Plumps_Die", true);
        }

        animator.SetTrigger("Boss_Plumps_Take_Damage");
    }

    public void DieAnimationEnded()
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
