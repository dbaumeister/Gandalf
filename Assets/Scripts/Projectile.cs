using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    Vector2 direction;
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    int damage;
    public Vector2 Direction { get => direction; set => direction = SanitizeDirection(value); }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

    Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
        if(rigidbody.position.x > 1000 || rigidbody.position.x < -1000 || rigidbody.position.y > 1000 || rigidbody.position.y < -1000)
        {
            killSelf();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.tag == "PlayerProjectile" && col.collider.tag == "Enemy") {
            Debug.Log("Hit ");
            killSelf(); }
        else if(gameObject.tag == "EnemyProjectile" && col.collider.tag == "Player")
        {
            Debug.Log("Hit one Heart");
            GameObject.FindGameObjectWithTag("GroupValues").GetComponent<GroupValues>().takeHearts(1);
           
            killSelf();
        }    
        else if(gameObject.tag == "EnemyProjectile" && col.collider.tag == "PlayerProjectile")
        {
            killSelf();
        }
        else if(gameObject.tag == "PlayerProjectile" && col.collider.tag == "EnemyProjectile")
        {
            killSelf();
        }
      
    
    }
    
    public void killSelf()
    {
        Destroy(this.gameObject);
    }
    public int getDamage()
    {
        return damage;
    }
}
