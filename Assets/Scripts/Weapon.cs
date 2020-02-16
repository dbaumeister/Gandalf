using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Attributes))]
[RequireComponent(typeof(Appearance))]
public class Weapon : MonoBehaviour
{
    Attributes attributes;
    Appearance appearance;

    float nextShotTime;

    [SerializeField]
    public Projectile projectilePrefab;

    Vector2 direction;
    public Vector2 Direction { get => GetActualDirection(); set => direction = value; }

    [SerializeField]
    Transform leftShootPoint;

    [SerializeField]
    Transform rightShootPoint;

    [SerializeField]
    Transform upShootPoint;

    [SerializeField]
    Transform downShootPoint;

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
            // 4 shoot points depending on the orientation of the player.
            Orientation orientation = GetOrientation();
            Transform point = null;
            switch(orientation)
            {
                case Orientation.Left: point = leftShootPoint; break;
                case Orientation.Right: point = rightShootPoint; break;
                case Orientation.Down: point = downShootPoint; break;
                case Orientation.Up: point = upShootPoint; break;
            }

            Projectile projectile = Instantiate(projectilePrefab);
            projectile.transform.position = point.position;
            if (attributes.ProjectileSpeed < 0)
            {
                projectile.MovementSpeed = Mathf.Max(attributes.ProjectileSpeed, (-1) * attributes.MaxAbsoluteProjectileSpeed);
            }
            else
            { projectile.MovementSpeed = Mathf.Min(attributes.ProjectileSpeed, attributes.MaxAbsoluteProjectileSpeed); }
            projectile.Direction = Direction;
            projectile.transform.localScale *= Mathf.Min(attributes.ProjectileSize, attributes.MaxProjectileSize);
            nextShotTime = Time.time + Mathf.Max(attributes.AttackDelay, attributes.MaxAttackDelay);
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

    enum Orientation
    {
        Up, Down, Left, Right
    }

    Orientation GetOrientation()
    {
        Vector2 dir = Direction;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
            {
                return Orientation.Right;
            }
            else
            {
                return Orientation.Left;
            }
        }
        else
        {
            if (dir.y > 0)
            {
                return Orientation.Up;
            }
            else
            {
                return Orientation.Down;
            }
        }
    }

    void Update()
    {
        appearance.Change(Direction);
        appearance.attacking = isAttacking;

        if(isAttacking)
        {
            Shoot();
        }
    }
}
