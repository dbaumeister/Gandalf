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

    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float AttackDelay { get => attackDelay; set => attackDelay = value; }
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public float ProjectileRange { get => projectileRange; set => projectileRange = value; }
    public float ProjectileSize { get => projectileSize; set => projectileSize = value; }
    public uint ProjectileCount { get => projectileCount; set => projectileCount = value; }
    public float WeaponDamage { get => weaponDamage; set => weaponDamage = value; }

    public Attributes GetCopy()
    {
        return (Attributes) this.MemberwiseClone();
    }

    public void Overwrite(Attributes other)
    {
        MovementSpeed = other.MovementSpeed;
        AttackDelay = other.AttackDelay;
        ProjectileSpeed = other.ProjectileSpeed;
        ProjectileRange = other.ProjectileRange;
        ProjectileSize = other.ProjectileSize;
        ProjectileCount = other.ProjectileCount;
        WeaponDamage = other.WeaponDamage;
    }

    public void Apply(Attributes other)
    {
        MovementSpeed += other.MovementSpeed;
        AttackDelay += other.AttackDelay;
        ProjectileSpeed += other.ProjectileSpeed;
        ProjectileRange += other.ProjectileRange;
        ProjectileSize += other.ProjectileSize;
        ProjectileCount += other.ProjectileCount;
        WeaponDamage += other.WeaponDamage;
    }

}
