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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Vector2 move = MovementSpeed * Direction * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + move);
    }
}
