using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BureaucratProjectile : MonoBehaviour
{
    [SerializeField]
    Vector2 direction;
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    int damage;
    public Vector2 Direction { get => direction; set => direction = SanitizeDirection(value); }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public int Damage { get => damage; set => damage = value; }

    [SerializeField]
    float lifeSpan;
    float startTime;
    Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startTime = Time.realtimeSinceStartup;
        
    }

    Vector2 SanitizeDirection(Vector2 dir)
    {
        if (dir == Vector2.zero)
        {
            return Vector2.down;
        }

        else return dir.normalized;
    }

    private void FixedUpdate()
    {
        Vector2 move = MovementSpeed * Direction * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + move);
      
        if (Time.realtimeSinceStartup-startTime >= lifeSpan)
        {
            killSelf();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.tag == "EnemyProjectile" && col.collider.tag == "Player")
        {
            col.gameObject.GetComponent<Appearance>().Hurt();
            GameObject.FindGameObjectWithTag("GroupValues").GetComponent<GroupValues>().takeHearts(1);
            
        }
        killSelf();
    }

    public void killSelf()
    {
        Destroy(this.gameObject);
    }
}

