using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SearchTarget : MonoBehaviour
{
    [SerializeField]
    GameObject nearestPlayer;
    [SerializeField]
    float speedEnemy;
    [SerializeField]
    float maxDistance;
    Vector2 direction;
    public Vector2 Direction { get => direction; set => direction = value; }
    int numberOfSteps;
    [SerializeField]
    int movementSpeed;
    float nextShotTime;
    [SerializeField]
    float projectileSpeed;
    [SerializeField]
    float projectileSize;
    [SerializeField]
    float attackDelay;
    [SerializeField]
    Projectile projectilePrefab;
    bool isAttacking;
    private int MAX_STEPS = 12;
    // Start is called before the first frame update
    void Start()
    {
        numberOfSteps = MAX_STEPS;
    }

    // Update is called once per frame
    void Update()
    {

        nearestPlayer = GetNearestPlayer();
        EvaluateDirection();
        if(nearestPlayer != null)
        {
            Shoot(nearestPlayer);
        }

    }

    
    private void FixedUpdate()
    {
        EnemyMove();
    }
    void EvaluateDirection()
    {
        if (nearestPlayer != null)
        {
            Direction = nearestPlayer.transform.position - this.gameObject.transform.position;
            numberOfSteps = 12;
        }
        else
        {
            numberOfSteps--;
            if (numberOfSteps == 0)
            {
                Direction = turn(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), this.gameObject);
                numberOfSteps = UnityEngine.Random.Range(1, 15);

            }
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
        Rigidbody2D enemyBody = this.gameObject.GetComponent<Rigidbody2D>();
                Vector2 move = movementSpeed * Direction * Time.fixedDeltaTime;
        enemyBody.MovePosition(Boundaries.ClampPosition(enemyBody.position + move));
    }
   
    void Shoot(GameObject nearestPlayer)
    {
        if (Time.time > nextShotTime)
        {
            Projectile projectile = Instantiate(projectilePrefab);
            projectile.transform.position = transform.position;
            projectile.MovementSpeed = projectileSpeed;
            projectile.Direction = Direction;
            projectile.transform.localScale = Vector3.one * projectileSize;
            nextShotTime = Time.time + attackDelay;
        }
    }

}


