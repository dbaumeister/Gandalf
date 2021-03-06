﻿
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy_SearchTarget : MonoBehaviour
{
    [SerializeField]
    int hitPoints;
    [SerializeField]
    GameObject nearestPlayer;
    [SerializeField]
    float maxDistance;
    Vector2 direction;
    public Vector2 Direction { get => direction; set => direction = value; }
    [SerializeField]
    float movementSpeed;
    float nextShotTime;
    [SerializeField]
    float projectileSpeed;
    [SerializeField]
    float projectileSize;
    [SerializeField]
    float attackDelay;
    [SerializeField]
    BureaucratProjectile burProjectilePrefab;
    bool isIdle;
    bool alreadyDead;


    NavMeshAgent agent;

    public delegate void Died();
    public event Died DieCallback;

    void Start()
    {
        isIdle = true;
        alreadyDead = false;

        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.destination = AcquireTargetPosition();
        agent.updatePosition = true;
        transform.rotation = Quaternion.identity;
    }

    Vector3 AcquireTargetPosition()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
        {
            return target.transform.position;
        }
        else return transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (nearestPlayer != null)
        {
            isIdle = false;
            Shoot(nearestPlayer);
        }
        nearestPlayer = GetNearestPlayer();
        EvaluateDirection();


    }
    
    void FixedUpdate()
    {
        EnemyMove();
    }

    void EvaluateDirection()
    {
        Direction = Vector2.zero;
        if (nearestPlayer != null)
        {
        
            Direction = nearestPlayer.transform.position - this.gameObject.transform.position;


            this.gameObject.GetComponent<SpriteRenderer>().flipX = Direction.x < 0;
            Animator anim = this.gameObject.GetComponent<Animator>();
            if (Direction.x * 1.2 < Direction.y && Direction.y > 0)
            {
                anim.SetBool("WalkingUpwards", true);
            }
            else if (anim.GetBool("WalkingUpwards"))
            {
                anim.SetBool("WalkingUpwards", false);
            }
        }
        else
        {
            isIdle = true;
        }
        
    }
    GameObject GetNearestPlayer()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        GameObject nextPlayerInRange = null;
        float currentDistance = maxDistance;
        foreach (GameObject curPlayer in allPlayers)
        {
            if (Vector2.Distance(this.gameObject.transform.position,
                     curPlayer.transform.position) <= currentDistance)

            {
                nextPlayerInRange = curPlayer;
                currentDistance = Vector2.Distance(this.gameObject.transform.position,
             curPlayer.transform.position);
            }

        }

        return nextPlayerInRange;
    }

    Vector2 turn(float switchX, float switchY, GameObject currentEnemy)
    {
        Vector2 reTurn = new Vector2();
        reTurn.x = currentEnemy.transform.position.x / currentEnemy.transform.position.y + switchX;
        reTurn.y = currentEnemy.transform.position.y / currentEnemy.transform.position.x + switchY;
        return reTurn;
    }

    void EnemyMove()
    {
        //Debug.Log(agent.SetDestination(AcquireTargetPosition()));
    }

    void Shoot(GameObject nearestPlayeraa)
    {
        if (Time.time > nextShotTime)
        {
            Animator animator = this.gameObject.GetComponent<Animator>();
            animator.SetBool("Attacking", true);
            BureaucratProjectile projectile = Instantiate(burProjectilePrefab);
            projectile.transform.position = transform.position;
            projectile.MovementSpeed = projectileSpeed;
            projectile.Direction = Direction;
            projectile.transform.localScale *= projectileSize;
            nextShotTime = Time.time + attackDelay;
            animator.SetBool("Attacking", false);
        }
    }
    public void TakeDamage(int damage)
    {
        hitPoints = hitPoints - damage;
        if (hitPoints <= 0 && !alreadyDead)
        {
            alreadyDead = true;
            Animator anim = this.gameObject.GetComponent<Animator>();
            anim.SetBool("Dead", true);

            StartCoroutine(DelayedRemove());
        }
    }
    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        Collider2D playerCollider = null;
        if (col.collider.tag == "Projectile")
        {
            Animator anim = this.gameObject.GetComponent<Animator>();
            anim.SetBool("Hit", true);
            playerCollider = col.collider;
            int damage = playerCollider.GetComponent<Projectile>().getDamage();
            TakeDamage(damage);
            StartCoroutine(DelayedContin());
            
        }
    }

    IEnumerator DelayedRemove()
    {
        yield return new WaitForSeconds(0.5f);
        if(DieCallback != null)
        {
            DieCallback();
        }
        Destroy(this.gameObject);


    }
    IEnumerator DelayedContin()
    {
        yield return new WaitForSeconds(0.3f);
        Animator anim = this.gameObject.GetComponent<Animator>();
        anim.SetBool("Hit", false);
    }
}


