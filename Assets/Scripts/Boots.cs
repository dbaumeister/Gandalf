using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Attributes))]
[RequireComponent(typeof(Rigidbody2D))]
public class Boots : MonoBehaviour
{
    Attributes attributes;
    Rigidbody2D rigidbody;

    Vector2 direction;
    public Vector2 Direction { get => direction; set => direction = value; }

    public void MovePerformed(InputAction.CallbackContext context)
    {
        Direction = context.ReadValue<Vector2>();
        if(Direction.magnitude < 0.1f)
        {
            Direction = Vector2.zero;
        }
    }

    void Start()
    {
        attributes = GetComponent<Attributes>();
        rigidbody = GetComponent<Rigidbody2D>();
        Direction = Vector2.zero;
    }

    void FixedUpdate()
    {
        Vector2 move = attributes.MovementSpeed * Direction * Time.fixedDeltaTime;
        rigidbody.MovePosition(Boundaries.ClampPosition(rigidbody.position + move));
    }
}
