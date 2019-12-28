using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSheep : MonoBehaviour

{
    [SerializeField]
    int hitPoints;
    [SerializeField]
    GameObject nearestPlayer;
    [SerializeField]
    float maxDistance;
    Vector2 direction;
    public Vector2 Direction { get => direction; set => direction = value; }    int numberOfSteps;
    [SerializeField]
    int movementSpeed;
    int attackSpeed;
    float nextShotTime;
    [SerializeField]
    float attackDelay;
    [SerializeField]
    int damageDealt;
    bool isIdle;
    bool isAttacking;


    // Start is called before the first frame update
    void Start()
    {
        isIdle = true;
        isAttacking = false;
        attackSpeed = 3 * movementSpeed;


    }

    // Update is called once per frame
    void Update()
    {
        /*if (nearestPlayer != null)
        {
            isIdle = false;

            Direction = Vector2.zero;
            Direction = nearestPlayer.transform.position - this.gameObject.transform.position;

            if (direction.x+direction.y < 15.0f)
            {
                Attack(nearestPlayer);
            }
          
        }
        else
        {
            nearestPlayer = GetNearestPlayer();
        }*/
        EvaluateDirection();


    }


    void FixedUpdate()
    {

        EnemyMove();
    }
    void EvaluateDirection()
    {
       Vector2 Direction = Vector2.zero;
        Direction.x = Random.Range(-1, 2)*5+1;
        Direction.y = Random.Range(-1, 2)*5+1;
        /*if (nearestPlayer != null)
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
            }*/
            //numberOfSteps = 12;
        //}
       // else
      //  {
         //   isIdle = true;
            /*  numberOfSteps--;
              if (numberOfSteps == 0)
              {
                  Direction = turn(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), this.gameObject);
                  numberOfSteps = UnityEngine.Random.Range(1, 15);

              }*/
       // }

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
        Rigidbody2D enemyBody = this.gameObject.GetComponent<Rigidbody2D>();
        Vector2 move = movementSpeed * Direction * Time.fixedDeltaTime;
        enemyBody.MovePosition(enemyBody.position + move);
    }

    void Attack(GameObject nearestPlayeraa)
    {
        if (Time.time > nextShotTime)
        {
            Animator animator = this.gameObject.GetComponent<Animator>();
            animator.SetBool("Attacking", true);
        
            nextShotTime = Time.time + attackDelay;
            animator.SetBool("Attacking", false);
        }
    }
    public void TakeDamage(int damage)
    {
        hitPoints = hitPoints - damage;
        if (hitPoints <= 0)
        {
            Animator anim = this.gameObject.GetComponent<Animator>();
            anim.SetBool("Dead", true);

            StartCoroutine(DelayedRemove());
        }
    }
    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        Collider2D playerCollider = null;
        if (col.collider.tag == "PlayerProjectile")
        {
            Animator anim = this.gameObject.GetComponent<Animator>();
            anim.SetBool("Hit", true);
            playerCollider = col.collider;
            int damage = playerCollider.GetComponent<Projectile>().getDamage();
            TakeDamage(damage);
            StartCoroutine(DelayedContin());

        }
        if(col.collider.tag == "Player" && isAttacking)
        {
            GameObject.FindGameObjectWithTag("GroupValues").GetComponent<GroupValues>().takeHearts(damageDealt);
        }
    }

    IEnumerator DelayedRemove()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);


    }
    IEnumerator DelayedContin()
    {
        yield return new WaitForSeconds(0.3f);
        Animator anim = this.gameObject.GetComponent<Animator>();
        anim.SetBool("Hit", false);
    }
}


