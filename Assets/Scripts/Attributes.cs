using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    [SerializeField]
    float attackDelay;

    [SerializeField]
    float projectileSpeed;

    [SerializeField]
    float projectileRange;

    [SerializeField]
    float projectileSize;

    [SerializeField]
    uint projectileCount;

    [SerializeField]
    float weaponDamage;


    [SerializeField]
    float maxAbsoluteSpeed = 16f;

    [SerializeField]
    float maxAttackDelay = 0.05f;

    [SerializeField]
    float maxAbsoluteProjectileSpeed = 40f;

    [SerializeField]
    float maxProjectileRange = 2;

    [SerializeField]
    float maxProjectileSize = 6f;

    [SerializeField]
    float maxWeaponDamage = 30;

    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float AttackDelay { get => attackDelay; set => attackDelay = value; }
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public float ProjectileRange { get => projectileRange; set => projectileRange = value; }
    public float ProjectileSize { get => projectileSize; set => projectileSize = value; }
    public uint ProjectileCount { get => projectileCount; set => projectileCount = value; }
    public float WeaponDamage { get => weaponDamage; set => weaponDamage = value; }
    public float MaxAbsoluteSpeed { get => maxAbsoluteSpeed; }
    public float MaxAttackDelay { get => maxAttackDelay;  }
    public float MaxAbsoluteProjectileSpeed { get => maxAbsoluteProjectileSpeed;  }
    public float MaxProjectileRange { get => maxProjectileRange;  }
    public float MaxProjectileSize { get => maxProjectileSize;  }
    public float MaxWeaponDamage { get => maxWeaponDamage;  }


    public void Start()
    {
        maxAbsoluteSpeed = 13f;
        maxAbsoluteProjectileSpeed = 30f;
        maxAttackDelay = 0.15f;
        maxProjectileRange = 2f;
        maxProjectileSize = 4f;
        maxWeaponDamage = 30f;
    }

    public Attributes GetCopy()
    {
        return (Attributes) this.MemberwiseClone();
    }

    public void Overwrite(Attributes other)
    {
        if (other.MovementSpeed < 0)
        {
            MovementSpeed = Mathf.Max(other.MovementSpeed, (-1) * maxAbsoluteSpeed);
        }
        else
        {
            MovementSpeed = Mathf.Min(other.MovementSpeed, maxAbsoluteSpeed);
        }
        AttackDelay = Mathf.Max(other.AttackDelay, maxAttackDelay);
        if(other.ProjectileSpeed < 0)
        {
            ProjectileSpeed = Mathf.Max(other.ProjectileSpeed, (-1) * maxAbsoluteProjectileSpeed);
        }
        else { ProjectileSpeed = Mathf.Min(other.ProjectileSpeed, maxAbsoluteProjectileSpeed); }

        ProjectileRange = Mathf.Min(other.ProjectileRange, maxProjectileRange);
        ProjectileSize = Mathf.Min(other.ProjectileSize, maxProjectileSize);
        ProjectileCount = other.ProjectileCount;
        WeaponDamage = Mathf.Min(other.WeaponDamage, maxWeaponDamage);
    }

    public void Apply(Attributes other)
    {
        if (other.MovementSpeed < 0)
        {
            MovementSpeed = Mathf.Max(other.MovementSpeed + movementSpeed, (-1) * maxAbsoluteSpeed);
        }
        else
        {
            MovementSpeed = Mathf.Min(other.MovementSpeed + movementSpeed, maxAbsoluteSpeed);
        }
        AttackDelay = Mathf.Max(other.AttackDelay + attackDelay, maxAttackDelay);
        if (other.ProjectileSpeed < 0)
        {
            ProjectileSpeed = Mathf.Max(other.ProjectileSpeed + projectileSpeed, (-1) * maxAbsoluteProjectileSpeed);
        }
        else { ProjectileSpeed = Mathf.Min(other.ProjectileSpeed + projectileSpeed, maxAbsoluteProjectileSpeed); }

        ProjectileRange = Mathf.Min(other.ProjectileRange + projectileRange, maxProjectileRange);
        ProjectileSize = Mathf.Min(other.ProjectileSize + projectileSize, maxProjectileSize);
        ProjectileCount = other.ProjectileCount;
        WeaponDamage = Mathf.Min(other.WeaponDamage + weaponDamage, maxWeaponDamage);
    }

}
