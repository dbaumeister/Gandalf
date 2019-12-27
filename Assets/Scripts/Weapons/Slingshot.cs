using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Attributes))]
[RequireComponent(typeof(Appearance))]
public class Slingshot extends Weapon : MonoBehaviour
{
    Attributes attributes;
    Appearance appearance;

    float nextShotTime;

    [SerializeField]
    Projectile projectilePrefab;

    Vector2 direction;
    public Vector2 Direction { get => GetActualDirection(); set => direction = value; }

    bool isAttacking;

    public void Shoot(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            isAttacking = true;
        }
        if(context.canceled)
        {
            isAttacking = false;
        }
    }

    void Shoot()
    {
        if(Time.time > nextShotTime)
        {
            Projectile projectile = Instantiate(projectilePrefab);
            projectile.transform.position = transform.position;
            projectile.MovementSpeed = attributes.ProjectileSpeed;
            projectile.Direction = Direction;
            projectile.transform.localScale = Vector3.one * attributes.ProjectileSize;
            nextShotTime = Time.time + attributes.AttackDelay;
        }
    }

    public void LookPerformed(InputAction.CallbackContext context)
    {
        Direction = context.ReadValue<Vector2>();
        if (Direction.magnitude < 0.1f)
        {
            Direction = Vector2.zero;
        }
    }

    void Start()
    {
        attributes = GetComponent<Attributes>();
        appearance = GetComponent<Appearance>();
        nextShotTime = 0;
        isAttacking = false;
        Direction = Vector2.zero;
    }

    Vector2 GetActualDirection()
    {
        Vector2 dir = direction;
        if (dir == Vector2.zero)
        {
            dir = GetComponent<Boots>().Direction;
        }
        return dir;
    }

    void Update()
    {
        appearance.Change(Direction);

        if(isAttacking)
        {
            Shoot();
        }
    }
}
